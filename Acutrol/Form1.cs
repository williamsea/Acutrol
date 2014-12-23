using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NationalInstruments.VisaNS;

namespace Acutrol
{

    /*
     * @author: Hai Tang (william.sea@jhu.edu)
     * Remarks:
     * 1.Channel 1 is the only channel in use. So the default channel is channel 1.
     * 
     * 
     */
    
    public partial class Form1 : Form
    {
        //GPIB Connection string
        string sAddress = "GPIB0::1::INSTR";
        //The VNA uses a message based session
        MessageBasedSession mbSession = null;
        //Open a generic Session first
        Session mySession = null;

        //Machine representation codes
        String Position = "P";
        String Rate = "R";
        String Acceleration = "A";
        String PositionMode = "P";
        String AbsRateMode = "A";
        String RelativeRateMode = "R";
        String RateMode = "R";
        String SynthesisMode = "S";

        //Parameters
        String RawPositionFeedback = "1081";
        String EstimatedPosition = "1082";
        String FilteredVelocityEstimate = "1038";
        String FilteredAccelEstimate = "1039";
        String ProfilerPositionCommend = "1008";
        String ProfilerVelocityCommend = "1009";
        String ProfilerAccelCommend = "1010";
        String MaximunPositionLimit = "1144";
        String MinimunPositionLimit = "1145";
        String PositionModeVelocityLimit = "1147";
        String PositionModeAccelLimit = "1148";
        String RateModeVelocityLimit = "1150";
        String RateModeAccelLimit = "1151";
        String SynthesisModeVelocityLimit = "1153";
        String SynthesisModeAccelLimit = "1154";


        public Form1()
        {
            InitializeComponent();
            comboBoxSelectMode.Text = "---Please Select Mode---";
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
        }

        private void initComboBoxWindows(ComboBox targetComboBox)
        {
            targetComboBox.Text = "---Please Assign Parameters---";
            targetComboBox.Items.Add("Raw Position Feedback");
            targetComboBox.Items.Add("Estimated Position");
            targetComboBox.Items.Add("Filtered Velocity Estimate");
            targetComboBox.Items.Add("Filtered Accel Estimate");
            targetComboBox.Items.Add("Profiler Position Commend");
            targetComboBox.Items.Add("Profiler Velocity Commend");
            targetComboBox.Items.Add("Profiler Accel Commend");
            targetComboBox.Items.Add("Maximun Position Limit");
            targetComboBox.Items.Add("Minimun Position Limit");
            targetComboBox.Items.Add("Position Mode Velocity Limit");
            targetComboBox.Items.Add("Position Mode Accel Limit");
            targetComboBox.Items.Add("Rate Mode Velocity Limit");
            targetComboBox.Items.Add("Rate Mode Accel Limit");
            targetComboBox.Items.Add("Synthesis Mode Velocity Limit");
            targetComboBox.Items.Add("Synthesis Mode Accel Limit");
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
            //open a Session to the VNA
            mySession = ResourceManager.GetLocalManager().Open(sAddress);
            //cast this to a message based session
            mbSession = (MessageBasedSession)mySession;
        }

        //Show (Read) the position, rate and acceleration of specific channel chosen
        private void ShowFrameAxis()
        {
            string responseString = null;
            responseString = ReadParameter(responseString, Position, textReadPos);   
            responseString = ReadParameter(responseString, Rate, textReadRate);
            responseString = ReadParameter(responseString, Acceleration, textReadAcc);
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
                // Toggle the hardware GPIB REN line.
                GpibSession gpib = (GpibSession)mySession;
                gpib.ControlRen(RenMode.DeassertAfterGtl);
                //Close the Session
                mbSession.Dispose();
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
            if (comboBoxSelectMode.SelectedItem == "Position Mode")
            {
                SetLimitations(PositionMode);
            }
            else if (comboBoxSelectMode.SelectedItem == "Relative Rate Mode" || comboBoxSelectMode.SelectedItem == "Absolute Rate Mode")
            {
                SetLimitations(RateMode);
            }
            else if (comboBoxSelectMode.SelectedItem == "Synthesis Mode")
            {
                SetLimitations(SynthesisMode);
            }
           
        }

        private void SetLimitations(String selectedMode)
        {
            try
            {
                //Set Limitations for different modes 
                mbSession.Write(":L :L " + selectedMode + " 1 " + textBoxLimitPosL.Text.ToString() + "; :L :H " + selectedMode + " 1 " + textBoxLimitPosH.Text.ToString() + " \n");
                mbSession.Write(":L :R " + selectedMode + " 1 " + textBoxLimitRate.Text.ToString() + " \n");
                mbSession.Write(":L :A " + selectedMode + " 1 " + textBoxLimitAcc.Text.ToString() + " \n");
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

    }
}
