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

        String Position = "P";
        String Rate = "R";
        String Acceleration = "A";
        String PositionMode = "P";
        String AbsRateMode = "A";
        String RelativeRateMode = "R";
        String SynthesisMode = "S";

        public Form1()
        {
            InitializeComponent();
            comboBoxSelectMode.Text = "---Please Select Mode---";
            comboBoxSelectMode.Items.Add("Position Mode");
            comboBoxSelectMode.Items.Add("Relative Rate Mode");
            comboBoxSelectMode.Items.Add("Absolute Rate Mode");
            comboBoxSelectMode.Items.Add("Synthesis Mode");
            //comboBoxSelectMode.SelectedIndex = 1;
            openMySession();
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
            responseString = ReadParameter(responseString, Position);
            textReadPos.Text = responseString;
            responseString = ReadParameter(responseString, Rate);
            textReadRate.Text = responseString;
            responseString = ReadParameter(responseString, Acceleration);
            textReadAcc.Text = responseString;
        }

        private string ReadParameter(string responseString, string targetParameter)
        {
            try
            {
                //Send target Query command
                mbSession.Write(":u:f f ; :R:"+targetParameter+" 1 \n");
                //Read the response
                responseString = mbSession.ReadString();  
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
             CommendParameter(Position);
             CommendParameter(Rate);
             CommendParameter(Acceleration);
        }


        private void CommendParameter(string targetParameter)
        {
            try
            {
                //Set target command
                 mbSession.Write(":D:"+targetParameter+" 1, " + textBoxSetPos.Text.ToString() + " \n");
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

    }
}
