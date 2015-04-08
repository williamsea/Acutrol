﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NationalInstruments.VisaNS;
using System.IO;

namespace Acutrol
{

    /*
     * @author: Hai Tang (william.sea@jhu.edu)
     * Remarks:
     * 1.Channel 1 is the only channel in use. So the default channel is channel 1.
     * 
     * TODO: 
     * 1.debug ECP87 remote touch commend: not working correctly. But when using loc mode touch screen, it's working correct
     * 2.text or sth else data store and review (display), where is the eye coil feedback signal?
     * 3.setup abs limits (1140,1141) and ROT/LIN.
     */

    public partial class Form1 : Form
    {
        //GPIB Connection string
        string sAddress = "GPIB0::1::INSTR";
        //The VNA uses a message based session
        MessageBasedSession mbSession = null;
        //Open a generic Session first
        Session mySession = null;

        //parameters for sinusoidal input
        double posReadValue = 1000;//used to check if position has been set back to zero position
        double cycleCounter = 0;//used for synthesis mode counting cycles
        //List<double> TargetCycleCount = new List<double>();
        //List<double> TargetMagn = new List<double>();
        //List<double> TargetFreq = new List<double>();
        double[] TargetCycleCount = new double[3] { 0, 0, 0 };//Must be initially Set to 0!!! Otherwise may cause problem!
        double[] TargetMagn = new double[3] { 0, 0, 0 };
        double[] TargetFreq = new double[3] { 0, 0, 0 };
        System.Timers.Timer sysTimer;
        int seqCount = 0; //count the sequence to be executed
        double zeroTrigger = 0.01;

        //Machine representation codes
        String Position = "P";
        String Rate = "R";
        String Acceleration = "A";
        String PositionMode = "P";
        String AbsRateMode = "A";
        String RelativeRateMode = "R";
        String RateMode = "R";
        String SynthesisMode = "S";
        String AbortMode = "A";

        //Parameters for contents of Display Windows
        String RawPositionFeedback = "1081";
        String EstimatedPosition = "1082";//default 1: position
        String EstimatedVelocity = "1083";//default 2: velocity
        String FilteredVelocityEstimate = "1038";
        String FilteredAccelEstimate = "1039";//default 3: acceleration
        String ProfilerPositionCommend = "1008";//default 4: position commend
        String ProfilerVelocityCommend = "1009";//default 5: velocity commend
        String ProfilerAccelCommend = "1010";//default 6: acceleration commend
        //Limits
        String MinimunPositionLimit = "1145"; String OvarMinPosLim = "1";
        String MaximunPositionLimit = "1144"; String OvarMaxPosLim = "2";
        String SynthesisModeVelocityLimit = "1153"; String OvarSynModeVeloLim = "3";
        String SynthesisModeAccelLimit = "1154"; String OvarSynModeAccLim = "4"; 
        String PositionModeVelocityLimit = "1147"; String OvarPosModeVeloLim = "5";
        //only 1-5 is allowed! for ECP variables
        String PositionModeAccelLimit = "1148"; String OvarPosModeAccLim = "6";
        String RateModeVelocityLimit = "1150"; String OvarRateModeVeloLim = "7";
        String RateModeAccelLimit = "1151"; String OvarRateModeAccLim = "8";
        String AbortModeVelocityLimit = "1159";
        String AbortModeAccelLimit = "1160";
        String VelocityAbsoluteLimit = "1140";
        String AccelAbsoluteLimit = "1141";
        String RateTripLimit = "1146";

        //display information
        double[] PosValArray = new double[100000]; //array used to store position values
        int disp = 0; //counter used for display data
        int counter = 0; //counter used to store data array
        int DispLength = 300; //30s

        //parameters for document saving and reading
        string savingPath = "C:\\Users\\VNEL\\Desktop\\Results";
        string readingPath;
        FileStream fileStream;
        StreamWriter streamWriter;
        Boolean RecordCtr = false;

        public Form1()
        {
            InitializeComponent();
            //comboBoxSelectMode.Text = "---Please Select Mode---";//Already set the default mode to be position mode
            comboBoxSelectMode.Text = "Position Mode (default)";
            comboBoxSelectMode.Items.Add("Position Mode");
            comboBoxSelectMode.Items.Add("Relative Rate Mode");
            comboBoxSelectMode.Items.Add("Absolute Rate Mode");
            comboBoxSelectMode.Items.Add("Synthesis Mode");

            initComboBoxWindows(comboBox_window1);
            initComboBoxWindows(comboBox_window2);
            initComboBoxWindows(comboBox_window3);
            initComboBoxWindows(comboBox_window4);
            initComboBoxWindows(comboBox_window5);
            initComboBoxWindows(comboBox_window6);

            //comboBoxSelectMode.SelectedIndex = 1;
            openMySession();

            ////Setup ECP 80 then 87 using TOUCH commend
            //mbSession.Write("u:t 179,179,176,32,152,144,32,50,176,181,176,32,152,151,32,50,176,181,181,181 \n");

            ////Set analog inputs gain to 0 
            //System.Threading.Thread.Sleep(10000);//wait for 10s for ECP to be restored
            //mbSession.Write(":u:t 180,177,178,50,32,144,32,176,181,179,50,32,144,32,176,181,181,181 \n");

            ////Set ROT/LIN value to 1 to use Limited Motion
            //System.Threading.Thread.Sleep(1000);//wait for 1s
            //mbSession.Write(":u:t 180,179,180,32,145,32,50,176,181,181,181 \n");

            ////Set the Veloc/Accel Absolute Limits
            //System.Threading.Thread.Sleep(1000);//wait for 1s
            //mbSession.Write(":u:t 180,180,148,176,177,32,145,144,32,51,176,180,49,176,181,181,181,181 \n");

            //set the default mode to be position mode
            SelectMode(PositionMode);

            ////Set default limitations just after initialization, before all the other actions
            //SetAllLimits();//default values

            //MessageBox.Show("ECP has been set to 80 then 87; Analog input 1&2 gain has been set to 0; ROT/LIN value set to 1 to use Limited Motion; Veloc/Accel Absolute Limits are default;  Default mode is Position Mode; All limits are set to default.");

            //Setup Ovariable
            //mbSession.Write(":c:o " + OvarMinPosLim + " , " + MinimunPositionLimit + " \n");
            //mbSession.Write(":c:o " + OvarMaxPosLim + " , " + MaximunPositionLimit + " \n");
            //mbSession.Write(":c:o " + OvarSynModeVeloLim + " , " + SynthesisModeVelocityLimit + " \n");
            //mbSession.Write(":c:o " + OvarSynModeAccLim + " , " + SynthesisModeAccelLimit + " \n");
            //mbSession.Write(":c:o " + OvarPosModeVeloLim + " , " + PositionModeVelocityLimit + " \n");//Allows only 5 data selects per ECP available for overiable config
            //mbSession.Write(":c:o " + OvarPosModeAccLim + " , " + PositionModeAccelLimit + " \n");
            //mbSession.Write(":c:o " + OvarRateModeVeloLim + " , " + RateModeVelocityLimit + " \n");
            //mbSession.Write(":c:o " + OvarRateModeAccLim + " , " + RateModeAccelLimit + " \n");

        }

        private void initComboBoxWindows(ComboBox targetComboBox)
        {
            targetComboBox.Text = "---Please Assign Parameters---";
            //Readings of position, rate, acceleration and their commends
            targetComboBox.Items.Add("Raw Position Feedback");
            targetComboBox.Items.Add("Estimated Position");
            targetComboBox.Items.Add("Estimated Velocity");
            targetComboBox.Items.Add("Filtered Velocity Estimate");
            targetComboBox.Items.Add("Filtered Accel Estimate");
            targetComboBox.Items.Add("Profiler Position Commend");
            targetComboBox.Items.Add("Profiler Velocity Commend");
            targetComboBox.Items.Add("Profiler Accel Commend");
            //Limitations
            targetComboBox.Items.Add("Maximun Position Limit");
            targetComboBox.Items.Add("Minimun Position Limit");
            targetComboBox.Items.Add("Position Mode Velocity Limit");
            targetComboBox.Items.Add("Position Mode Accel Limit");
            targetComboBox.Items.Add("Rate Mode Velocity Limit");
            targetComboBox.Items.Add("Rate Mode Accel Limit");
            targetComboBox.Items.Add("Synthesis Mode Velocity Limit");
            targetComboBox.Items.Add("Synthesis Mode Accel Limit");
            targetComboBox.Items.Add("Abort Mode Velocity Limit");
            targetComboBox.Items.Add("Abort Mode Accel Limit");
            targetComboBox.Items.Add("Velocity Absolute Limit");
            targetComboBox.Items.Add("Accel Absolute Limit");
            targetComboBox.Items.Add("Rate Trip Limit");

        }

        private void comboBoxSelectMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSelectMode.SelectedItem == "Position Mode")
            {
                SelectMode(PositionMode);
            }
            else if (comboBoxSelectMode.SelectedItem == "Relative Rate Mode")
            {
                SelectMode(RelativeRateMode);
            }
            else if (comboBoxSelectMode.SelectedItem == "Absolute Rate Mode")
            {
                SelectMode(AbsRateMode);
            }
            else if (comboBoxSelectMode.SelectedItem == "Synthesis Mode")
            {
                SelectMode(SynthesisMode);
            }
        }

        private void SelectMode(String chosenMode)
        {
            try
            {
                mbSession.Write(":M:"+chosenMode+" 1 \n");
            }

            catch (VisaException v_exp)
            {
                MessageBox.Show(v_exp.Message);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }


        private void openMySession()
        {
            closeMySession();

            //open a Session to the VNA
            mySession = ResourceManager.GetLocalManager().Open(sAddress);
            //cast this to a message based session
            mbSession = (MessageBasedSession)mySession;
        }

        private void closeMySession()
        {
            if (mbSession == null)
                return;

            // Toggle the hardware GPIB REN line. Return to Local.
            GpibSession gpib = (GpibSession)mySession;
            gpib.ControlRen(RenMode.DeassertAfterGtl);

            //Close the Session
            mbSession.Dispose();
            mbSession = null;
        }

        //Show (Read) the position, rate and acceleration of specific channel chosen
        private void ShowFrameAxis()
        {
            string responseString = null;

            responseString = ReadParameter(responseString, Position, textReadPos);
            posReadValue = Convert.ToDouble(responseString);

            //record position into txt file
            if (RecordCtr == true)
            {
                streamWriter.Write(responseString + ' ');//write position string
            }
            PosValArray[counter] = posReadValue;
            
            responseString = ReadParameter(responseString, Rate, textReadRate);
            responseString = ReadParameter(responseString, Acceleration, textReadAcc);

            counter++;
            DisplayData();

            //display cycle counts
            textBox_cycle_counter.Text = cycleCounter.ToString();
            /*Read Ovariables*/
            //responseString = ReadOvarParameter(responseString, OvarMinPosLim, textBoxPosLimLow);
            //responseString = ReadOvarParameter(responseString, OvarMaxPosLim, textBoxPosLimHigh);
            //responseString = ReadOvarParameter(responseString, OvarSynModeVeloLim, textBoxSynModeRateLim);
            //responseString = ReadOvarParameter(responseString, OvarSynModeAccLim, textBoxSynModeAccLim);
            //responseString = ReadOvarParameter(responseString, OvarPosModeVeloLim, textBoxPosModeRateLim); //Allows only 5 data selects per ECP available for overiable config
            //responseString = ReadOvarParameter(responseString, OvarPosModeAccLim, textBoxPosModeAccLim); 
            //responseString = ReadOvarParameter(responseString, OvarRateModeVeloLim, textBoxRateModeRateLim); 
            //responseString = ReadOvarParameter(responseString, OvarRateModeAccLim, textBoxRateModeAccLim); 
        }

        //display position in waveform format
        private void DisplayData()
        {
            this.pos_chart.Series["PosVal"].Points.Clear();

            if (counter >= DispLength)
            {
                for (disp = counter - (DispLength); disp < counter; disp++)
                {
                    this.pos_chart.Series["PosVal"].Points.AddXY((disp / 10).ToString(), PosValArray[disp]);
                }
            }
            else
            {
                for (disp = 0; disp < DispLength; disp++)
                {
                    this.pos_chart.Series["PosVal"].Points.AddXY((disp / 10).ToString(), PosValArray[disp]);
                }
            }  
        }

        private string ReadParameter(string responseString, string targetParameter, TextBox targetTextBox)
        {
            try
            {
                //Send target Query command
                mbSession.Write(":u:f f ; :R:"+targetParameter+" 1 \n");
                //Read the response
                responseString = mbSession.ReadString();
                targetTextBox.Text = responseString;
            }
            catch (VisaException v_exp)
            {
                MessageBox.Show(v_exp.Message);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            return responseString;
        }

        private string ReadOvarParameter(string responseString, string Ovariable, TextBox targetTextBox)
        {
            try
            {
                //Send target Query command
                mbSession.Write(":u:f f ; :r:o " + Ovariable + " \n");
                //Read the response
                responseString = mbSession.ReadString();
                targetTextBox.Text = responseString;
            }
            catch (VisaException v_exp)
            {
                MessageBox.Show(v_exp.Message);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            return responseString;
        }

        private void ShowAxis_Tick(object sender, EventArgs e)
        {
            ShowFrameAxis();
        }

        private void SetAxisParameters()
        {
            CommendParameter(Position, textBoxSetPos);
            CommendParameter(Rate, textBoxSetRate);
            CommendParameter(Acceleration, textBoxSetAcc);
        }


        private void CommendParameter(string targetParameter, TextBox targetTextBox)
        {
            try
            {
                //Set target command
                mbSession.Write(":D:" + targetParameter + " 1, " + targetTextBox.Text.ToString() + " \n");
            }
            catch (VisaException v_exp)
            {
                MessageBox.Show(v_exp.Message);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void ExecuteCommendButton_Click(object sender, EventArgs e)
        {
            SetAxisParameters();
        }

        

        private void ReturnLocalButton_Click(object sender, EventArgs e)
        {

            try
            {
                closeMySession();
                ShowAxis.Enabled = false;
            }
            catch (VisaException v_exp)
            {
                MessageBox.Show(v_exp.Message);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

        }

        private void SinInputButton_Click(object sender, EventArgs e)
        {
             CommendSinusoidal();
        }

        private void CommendSinusoidal()
        {
            try
            {
                //Set Magnitude, Frequency and Phase for sinusoidal input (default channel 1)
                 mbSession.Write(":D:O 1, " + textBoxSetMagn.Text.ToString() + ", " + textBoxSetFreq.Text.ToString() + ", " + textBoxSetPhase.Text.ToString() + " \n");
            }
            catch (VisaException v_exp)
            {
                MessageBox.Show(v_exp.Message);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void RemoteMode_Click(object sender, EventArgs e)
        {
            openMySession();
            ShowAxis.Enabled = true;
        }

        private void ExecuteLimitButton_Click(object sender, EventArgs e)
        {
            /*The following code set the limitations for all modes with the same choen limited values*/
            SetAllLimits();

            /*The following code set the limitations under a specific mode, i.e Pos, Velo, Acc value under one of Pos, Rate, Syn modes*/
            //if (comboBoxSelectMode.SelectedItem == "Position Mode")
            //{
            //    SetLimitations(PositionMode);
            //}
            //else if (comboBoxSelectMode.SelectedItem == "Relative Rate Mode" || comboBoxSelectMode.SelectedItem == "Absolute Rate Mode")
            //{
            //    SetLimitations(RateMode);
            //}
            //else if (comboBoxSelectMode.SelectedItem == "Synthesis Mode")
            //{
            //    SetLimitations(SynthesisMode);
            //}
           
        }

        private void SetAllLimits()
        {
            mbSession.Write(":L :L " + PositionMode + " 1 " + textBoxLimitPosL.Text.ToString() + "; :L :H " + PositionMode + " 1 " + textBoxLimitPosH.Text.ToString() + " \n");
            mbSession.Write(":L :R " + PositionMode + " 1 " + textBoxLimitRate.Text.ToString() + " \n");
            mbSession.Write(":L :A " + PositionMode + " 1 " + textBoxLimitAcc.Text.ToString() + " \n");

            mbSession.Write(":L :L " + RateMode + " 1 " + textBoxLimitPosL.Text.ToString() + "; :L :H " + RateMode + " 1 " + textBoxLimitPosH.Text.ToString() + " \n");
            mbSession.Write(":L :R " + RateMode + " 1 " + textBoxLimitRate.Text.ToString() + " \n");
            mbSession.Write(":L :A " + RateMode + " 1 " + textBoxLimitAcc.Text.ToString() + " \n");

            mbSession.Write(":L :L " + SynthesisMode + " 1 " + textBoxLimitPosL.Text.ToString() + "; :L :H " + SynthesisMode + " 1 " + textBoxLimitPosH.Text.ToString() + " \n");
            mbSession.Write(":L :R " + SynthesisMode + " 1 " + textBoxLimitRate.Text.ToString() + " \n");
            mbSession.Write(":L :A " + SynthesisMode + " 1 " + textBoxLimitAcc.Text.ToString() + " \n");

            mbSession.Write(":L :R " + AbortMode + " 1 " + textBoxAbortRateLmt.Text.ToString() + " \n");
            mbSession.Write(":L :A " + AbortMode + " 1 " + textBoxAbortAccLmt.Text.ToString() + " \n");

            mbSession.Write(":L :V " + PositionMode + " 1 " + textBoxVTripLmt.Text.ToString() + " \n");
            mbSession.Write(":L :V " + RateMode + " 1 " + textBoxVTripLmt.Text.ToString() + " \n");
            mbSession.Write(":L :V " + SynthesisMode + " 1 " + textBoxVTripLmt.Text.ToString() + " \n");

        }

        /*The following code set the limitations under a specific mode, i.e Pos, Velo, Acc value under one of Pos, Rate, Syn modes*/
        //private void SetLimitations(String selectedMode)
        //{
        //    try
        //    {
        //        //Set Limitations for different modes 
        //        mbSession.Write(":L :L " + selectedMode + " 1 " + textBoxLimitPosL.Text.ToString() + "; :L :H " + selectedMode + " 1 " + textBoxLimitPosH.Text.ToString() + " \n");
        //        mbSession.Write(":L :R " + selectedMode + " 1 " + textBoxLimitRate.Text.ToString() + " \n");
        //        mbSession.Write(":L :A " + selectedMode + " 1 " + textBoxLimitAcc.Text.ToString() + " \n");
        //    }
        //    catch (VisaException v_exp)
        //    {
        //        MessageBox.Show(v_exp.Message);
        //    }
        //    catch (Exception exp)
        //    {
        //        MessageBox.Show(exp.Message);
        //    }
        //}

        #region WindowDisplays
        private void comboBox_window1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetupWindows("1", comboBox_window1);
        }

        private void comboBox_window2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetupWindows("2", comboBox_window2);
        }

        private void comboBox_window3_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetupWindows("3", comboBox_window3);
        }

        private void comboBox_window4_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetupWindows("4", comboBox_window4);
        }

        private void comboBox_window5_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetupWindows("5", comboBox_window5);
        }

        private void comboBox_window6_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetupWindows("6", comboBox_window6);
        }

        private void SetupWindows(String windowNum, ComboBox targetComboBox)
        {
            try
            {

                if (targetComboBox.SelectedItem == "Raw Position Feedback")
                {
                    mbSession.Write(":C:W " + windowNum + ", " + RawPositionFeedback + "  \n");
                }
                else if (targetComboBox.SelectedItem == "Estimated Position")
                {
                    mbSession.Write(":C:W " + windowNum + ", " + EstimatedPosition + "  \n");
                }
                else if (targetComboBox.SelectedItem == "Estimated Velocity")
                {
                    mbSession.Write(":C:W " + windowNum + ", " + EstimatedVelocity + "  \n");
                }
                else if (targetComboBox.SelectedItem == "Filtered Velocity Estimate")
                {
                    mbSession.Write(":C:W " + windowNum + ", " +FilteredVelocityEstimate + "  \n");
                }
                else if (targetComboBox.SelectedItem == "Filtered Accel Estimate")
                {
                    mbSession.Write(":C:W " + windowNum + ", " + FilteredAccelEstimate + "  \n");
                }
                else if (targetComboBox.SelectedItem == "Profiler Position Commend")
                {
                    mbSession.Write(":C:W " + windowNum + ", " + ProfilerPositionCommend + "  \n");
                }
                else if (targetComboBox.SelectedItem == "Profiler Velocity Commend")
                {
                    mbSession.Write(":C:W " + windowNum + ", " + ProfilerVelocityCommend + "  \n");
                }
                else if (targetComboBox.SelectedItem == "Profiler Accel Commend")
                {
                    mbSession.Write(":C:W " + windowNum + ", " + ProfilerAccelCommend + "  \n");
                }
                else if (targetComboBox.SelectedItem == "Maximun Position Limit")
                {
                    mbSession.Write(":C:W " + windowNum + ", " + MaximunPositionLimit + "  \n");
                }
                else if (targetComboBox.SelectedItem == "Minimun Position Limit")
                {
                    mbSession.Write(":C:W " + windowNum + ", " + MinimunPositionLimit + "  \n");
                }
                else if (targetComboBox.SelectedItem == "Position Mode Velocity Limit")
                {
                    mbSession.Write(":C:W " + windowNum + ", " + PositionModeVelocityLimit + "  \n");
                }
                else if (targetComboBox.SelectedItem == "Position Mode Accel Limit")
                {
                    mbSession.Write(":C:W " + windowNum + ", " + PositionModeAccelLimit + "  \n");
                }
                else if (targetComboBox.SelectedItem == "Rate Mode Velocity Limit")
                {
                    mbSession.Write(":C:W " + windowNum + ", " + RateModeVelocityLimit + "  \n");
                }
                else if (targetComboBox.SelectedItem == "Rate Mode Accel Limit")
                {
                    mbSession.Write(":C:W " + windowNum + ", " + RateModeAccelLimit + "  \n");
                }
                else if (targetComboBox.SelectedItem == "Synthesis Mode Velocity Limit")
                {
                    mbSession.Write(":C:W " + windowNum + ", " + SynthesisModeVelocityLimit + "  \n");
                }
                else if (targetComboBox.SelectedItem == "Synthesis Mode Accel Limit")
                {
                    mbSession.Write(":C:W " + windowNum + ", " + SynthesisModeAccelLimit + "  \n");
                }
                else if (targetComboBox.SelectedItem == "Abort Mode Velocity Limit")
                {
                    mbSession.Write(":C:W " + windowNum + ", " + AbortModeVelocityLimit + "  \n");
                }
                else if (targetComboBox.SelectedItem == "Abort Mode Accel Limit")
                {
                    mbSession.Write(":C:W " + windowNum + ", " + AbortModeAccelLimit +"  \n");
                }
                else if (targetComboBox.SelectedItem == "Velocity Absolute Limit")
                {
                    mbSession.Write(":C:W " + windowNum + ", " +VelocityAbsoluteLimit +"  \n");
                }
                else if (targetComboBox.SelectedItem == "Accel Absolute Limit")
                {
                    mbSession.Write(":C:W " + windowNum + ", " +AccelAbsoluteLimit +"  \n");
                }
                else if (targetComboBox.SelectedItem == "Rate Trip Limit")
                {
                    mbSession.Write(":C:W " + windowNum + ", " + RateTripLimit + "  \n");
                }

            }
            catch (VisaException v_exp)
            {
                MessageBox.Show(v_exp.Message);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        #endregion

        private void button_default_windows_Click(object sender, EventArgs e)
        {
            mbSession.Write(":C:W 1, " + EstimatedPosition + "  \n");
            mbSession.Write(":C:W 2, " + EstimatedVelocity + "  \n");
            mbSession.Write(":C:W 3, " + FilteredAccelEstimate + "  \n");
            mbSession.Write(":C:W 4, " + ProfilerPositionCommend + "  \n");
            mbSession.Write(":C:W 5, " + ProfilerVelocityCommend + "  \n");
            mbSession.Write(":C:W 6, " + ProfilerAccelCommend + "  \n");
        }

        private void button_showLimits_Click(object sender, EventArgs e)
        {
            mbSession.Write(":C:W 1, " + MinimunPositionLimit + "  \n");
            //mbSession.Write(":C:W 2, " + MaximunPositionLimit + "  \n");
            mbSession.Write(":C:W 2, " + RateTripLimit + "  \n");
            mbSession.Write(":C:W 3, " + SynthesisModeVelocityLimit + "  \n");
            mbSession.Write(":C:W 4, " + SynthesisModeAccelLimit + "  \n");
            //mbSession.Write(":C:W 5, " + AbortModeVelocityLimit + "  \n");
            mbSession.Write(":C:W 5, " + VelocityAbsoluteLimit + "  \n");
            mbSession.Write(":C:W 6, " + AbortModeAccelLimit + "  \n");
            //mbSession.Write(":C:W 5, " + PositionModeVelocityLimit + "  \n");
            //mbSession.Write(":C:W 6, " + PositionModeAccelLimit + "  \n");
            //mbSession.Write(":C:W 5, " + RateModeVelocityLimit + "  \n");
            //mbSession.Write(":C:W 6, " + RateModeAccelLimit + "  \n");
        }

        private void button_reset_limits_Click(object sender, EventArgs e)
        {
            textBoxLimitPosL.Clear();
            textBoxLimitPosH.Clear();
            textBoxLimitRate.Clear();
            textBoxLimitAcc.Clear();
        }

        private void edit_default_button_Click(object sender, EventArgs e)
        {

        }

        private void button_interlock_open_Click(object sender, EventArgs e)
        {
            Interlock_Open();
            RecordCtr = false;
        }

        private void Interlock_Open()
        {
            if (mySession == null)
                openMySession();

            mbSession.Write(":I:O 1  \n");
        }

        private void button_interlock_close_Click(object sender, EventArgs e)
        {
            Interlock_Close();
        }

        private void Interlock_Close()
        {
            if (mySession == null)
                openMySession();

            mbSession.Write(":I:C 1  \n");
        }

        private void button_interlock_reset_Click(object sender, EventArgs e)
        {
            if (mySession == null)
                openMySession();

            mbSession.Write(":I:R \n");
        }

        private void button_ECP_Click(object sender, EventArgs e)
        {
            if (mySession == null)
                openMySession();
            ////Setup ECP 80 then 87 using TOUCH commend
            //mbSession.Write("u:t 179,179,176,32,152,144,32,50,176,181,176,32,152,151,32,50,176,181,181,181 \n");

            //Setup ECP 87 using TOUCH commend
            mbSession.Write("u:t 179,179,176,32,152,151,32,50,176,181,181,181 \n");
            System.Threading.Thread.Sleep(5000);
        }

        private void button_cut_analog_input_Click(object sender, EventArgs e)
        {
            if (mySession == null)
                openMySession();
            //Set analog inputs gain to 0 
            //System.Threading.Thread.Sleep(10000);//wait for 10s for ECP to be restored
            mbSession.Write(":u:t 180,177,178,50,32,144,32,176,181,179,50,32,144,32,176,181,181,181 \n");
        }

        private void buttonSinuSeqExecute_Click(object sender, EventArgs e)
        {
            TargetMagn[0] = Convert.ToDouble(textBoxSeqMag1.Text);
            TargetMagn[1] = Convert.ToDouble(textBoxSeqMag2.Text);
            TargetMagn[2] = Convert.ToDouble(textBoxSeqMag3.Text);
            TargetFreq[0] = Convert.ToDouble(textBoxSeqFreq1.Text);
            TargetFreq[1] = Convert.ToDouble(textBoxSeqFreq2.Text);
            TargetFreq[2] = Convert.ToDouble(textBoxSeqFreq3.Text);
            TargetCycleCount[0] = Convert.ToDouble(textBoxSeqCycle1.Text);
            TargetCycleCount[1] = Convert.ToDouble(textBoxSeqCycle2.Text);
            TargetCycleCount[2] = Convert.ToDouble(textBoxSeqCycle3.Text);

            //open text writer
            WriteIntoTxt();

            //Set all limitations
            SetAllLimits();

            //Go back to zero position under position mode
            textBoxSetPos.Text = "0"; //need to set earlier before commend, it takes some time to write
            SelectMode(PositionMode);
            CommendParameter(Position, textBoxSetPos);
            Interlock_Close();

            CheckZeroPosition.Enabled = true; //Once go back to zero, start sinusoidal cycles

            cycleCounter = 0;
        }

        private void WriteIntoTxt()
        {
            if (!Directory.Exists(savingPath))
            {
                Directory.CreateDirectory(savingPath);
            }
            string path = savingPath + "\\" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss") + ".txt";
            fileStream = new FileStream(path, System.IO.FileMode.Create);
            streamWriter = new StreamWriter(fileStream);
            RecordCtr = true;//start recording position into txt
        }

        private void updateLimits(String newRateLimit, String newAccLimit){
            textBoxLimitRate.Text = newRateLimit;
            textBoxLimitAcc.Text = newAccLimit;
            SetAllLimits();
        }

        private void CheckZeroPosition_Tick(object sender, EventArgs e)
        {
            ExecuteOneSequence(TargetMagn[seqCount], TargetFreq[seqCount]);
        }

        private void ExecuteOneSequence(double targetMagn, double targetFreq)
        {
            if (Math.Abs(posReadValue) < zeroTrigger)
            {
                SelectMode(SynthesisMode); comboBoxSelectMode.Text = "Synthesis Mode";
                textBoxSetMagn.Text = targetMagn.ToString();//need to set earlier before commmend, it takes some time to write
                textBoxSetFreq.Text = targetFreq.ToString();

                //set up the timer used for cycle counting based on target frequency
                sysTimer = new System.Timers.Timer(1000 / targetFreq); //so that the time interval = 1/f (sec)
                sysTimer.Elapsed += new System.Timers.ElapsedEventHandler(theCount);
                sysTimer.AutoReset = true; // false:execute only once. true: keep execute

                updateLimits("150", "1200");
                System.Threading.Thread.Sleep(50);//wait for calculation and write
                CommendSinusoidal();

                CheckZeroPosition.Enabled = false;
                CheckCycleCount.Enabled = true;
                sysTimer.Enabled = true;
            }

        }

        private void theCount(object source, System.Timers.ElapsedEventArgs e)
        {
            cycleCounter++; //increased by one in every time interval = 1/f (sec)
        }

        private void CheckCycleCount_Tick(object sender, EventArgs e)
        {
            if (cycleCounter == TargetCycleCount[seqCount] - 5)//start slowing down 5 cycles before finishing
            {
                textBoxSetMagn.Text = "0";
                CommendSinusoidal();
            }
            if (cycleCounter == TargetCycleCount[seqCount])
            {
                //go back to zero position again
                textBoxSetPos.Text = "0";
                updateLimits("20", "50");
                SelectMode(PositionMode); comboBoxSelectMode.Text = "Position Mode";
                sysTimer.Enabled = false;
                CheckCycleCount.Enabled = false;
                CommendParameter(Position, textBoxSetPos);

                ReturnZeroPosition.Enabled = true;
            }
        }

        private void ReturnZeroPosition_Tick(object sender, EventArgs e)
        {
            if (seqCount < 3 && Math.Abs(posReadValue) < zeroTrigger)
            {
                seqCount++;
                cycleCounter = 0;
                CheckZeroPosition.Enabled = true;//go to the next sequence
                ReturnZeroPosition.Enabled = false;
            }

            if (seqCount == 3 && Math.Abs(posReadValue) < zeroTrigger)
            {
                Interlock_Open();
                CheckZeroPosition.Enabled = false;//no need to go back
                ReturnZeroPosition.Enabled = false;

                //stop position recording and dispose
                RecordCtr = false;
                streamWriter.Close();
                fileStream.Close();
                seqCount = 0;
            }
        }

        private void button_select_file_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = savingPath;
            openFileDialog.Filter = "txt Files(*.txt)|*.txt|All Files(*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                readingPath = openFileDialog.FileName;
            }
            textBoxReadingDirectory.Text = readingPath;
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        }

        private void button_display_file_Click(object sender, EventArgs e)
        {
            ShowAxis.Enabled = false;
            if (textBoxReadingDirectory.Text != "")
            {
                readingPath = textBoxReadingDirectory.Text;
                Read_Txt();
            }
            else
                MessageBox.Show("Please input a path of data file!");
        }

        private void Read_Txt()
        {
            StreamReader sr = new StreamReader(readingPath, Encoding.Default);
            string wholeString;
            string[] readPosValArray;
            Array.Clear(PosValArray, 0, PosValArray.Length);//clear original PosValArray
            wholeString = sr.ReadToEnd();//generate a string containing all the info in the file
            readPosValArray = wholeString.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);//split the whole string into a string array
            PosValArray = Array.ConvertAll(readPosValArray, Double.Parse);//convert string[] to double[]
            sr.Close();
            DisplayFileData();
        }
        //display position in waveform format
        private void DisplayFileData()
        {
            this.pos_chart.Series["PosVal"].Points.Clear();
            for (disp = 0; disp < PosValArray.Length; disp++)
            {
                this.pos_chart.Series["PosVal"].Points.AddXY((disp / 10).ToString(), PosValArray[disp]);
            }
        }

        private void button_return_realtime_Click(object sender, EventArgs e)
        {
            this.pos_chart.Series["PosVal"].Points.Clear();
            ShowAxis.Enabled = true;
        }
    }
}
