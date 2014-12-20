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

        public Form1()
        {
            InitializeComponent();
            comboBoxSelectMode.Items.Add("Position Mode");
            comboBoxSelectMode.Items.Add("Relative Rate Mode");
            comboBoxSelectMode.Items.Add("Absolute Rate Mode");
            comboBoxSelectMode.Items.Add("Synthesis Mode");
        }

        private void comboBoxSelectMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSelectMode.SelectedItem == "Position Mode")
            {
                //GPIB Connection string
                string sAddress = "GPIB0::1::INSTR";
                //The VNA uses a message based session
                MessageBasedSession mbSession = null;
                //Open a generic Session first
                Session mySession = null;

                try
                {
                    //open a Session to the VNA
                    mySession = ResourceManager.GetLocalManager().Open(sAddress);
                    //cast this to a message based session
                    mbSession = (MessageBasedSession)mySession;
                    //Send Position Query command
                    mbSession.Write(":M:P 1 \n");
                    //Close the Session
                    mbSession.Dispose();
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
            else if (comboBoxSelectMode.SelectedItem == "Relative Rate Mode")
            {
                //GPIB Connection string
                string sAddress = "GPIB0::1::INSTR";
                //The VNA uses a message based session
                MessageBasedSession mbSession = null;
                //Open a generic Session first
                Session mySession = null;

                try
                {
                    //open a Session to the VNA
                    mySession = ResourceManager.GetLocalManager().Open(sAddress);
                    //cast this to a message based session
                    mbSession = (MessageBasedSession)mySession;
                    //Send Position Query command
                    mbSession.Write(":M:R 1 \n");
                    //Close the Session
                    mbSession.Dispose();
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
            else if (comboBoxSelectMode.SelectedItem == "Absolute Rate Mode")
            {
                //GPIB Connection string
                string sAddress = "GPIB0::1::INSTR";
                //The VNA uses a message based session
                MessageBasedSession mbSession = null;
                //Open a generic Session first
                Session mySession = null;

                try
                {
                    //open a Session to the VNA
                    mySession = ResourceManager.GetLocalManager().Open(sAddress);
                    //cast this to a message based session
                    mbSession = (MessageBasedSession)mySession;
                    //Send Position Query command
                    mbSession.Write(":M:A 1 \n");
                    //Close the Session
                    mbSession.Dispose();
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
            else if (comboBoxSelectMode.SelectedItem == "Synthesis Mode")
            {
                //GPIB Connection string
                string sAddress = "GPIB0::1::INSTR";
                //The VNA uses a message based session
                MessageBasedSession mbSession = null;
                //Open a generic Session first
                Session mySession = null;

                try
                {
                    //open a Session to the VNA
                    mySession = ResourceManager.GetLocalManager().Open(sAddress);
                    //cast this to a message based session
                    mbSession = (MessageBasedSession)mySession;
                    //Send Position Query command
                    mbSession.Write(":M:S 1 \n");
                    //Close the Session
                    mbSession.Dispose();
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

        //Show (Read) the position, rate and acceleration of specific channel chosen
        private void ShowFrameAxis()
        {
            //GPIB Connection string
            string sAddress = "GPIB0::1::INSTR";

            //The VNA uses a message based session
            MessageBasedSession mbSession = null;
            //Open a generic Session first
            Session mySession = null;

            //response string
            string responseString = null;

            try
            {
                //open a Session to the VNA
                mySession = ResourceManager.GetLocalManager().Open(sAddress);
                //cast this to a message based session
                mbSession = (MessageBasedSession)mySession;
                //Send Position Query command
                mbSession.Write(":u:f f ; :R:P 1 \n");
                //Read the response
                responseString = mbSession.ReadString();
                //Write to Textbox
                textReadPos.Text = responseString;

                //Send Rate Query command
                mbSession.Write(":u:f f ; :R:R 1 \n");
                //Read the response
                responseString = mbSession.ReadString();
                //Write to Textbox
                textReadRate.Text = responseString;

                //Send Acceleration Query command
                mbSession.Write(":u:f f ; :R:A 1 \n");
                //Read the response
                responseString = mbSession.ReadString();
                //Write to Textbox
                textReadAcc.Text = responseString;


                // Toggle the hardware GPIB REN line.
                GpibSession gpib = (GpibSession)mySession;
                gpib.ControlRen(RenMode.DeassertAfterGtl);



                //Close the Session
                mbSession.Dispose();
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

        private void ShowAxis_Tick(object sender, EventArgs e)
        {
            ShowFrameAxis();
        }

        private void SetAxisParameters()
        {
            
            //GPIB Connection string
            string sAddress = "GPIB0::1::INSTR";

            //The VNA uses a message based session
            MessageBasedSession mbSession = null;
            //Open a generic Session first
            Session mySession = null;
            
            try
            {
                //open a Session to the VNA
                mySession = ResourceManager.GetLocalManager().Open(sAddress);
                //cast this to a message based session
                mbSession = (MessageBasedSession)mySession;
                //Set Position command
                mbSession.Write(":D:P 1, " + textBoxSetPos.Text.ToString() + " \n");

                //Set Rate command
                mbSession.Write(":D:R 1, " + textBoxSetRate.Text.ToString() + " \n");

                //Set Acceleration command
                mbSession.Write(":D:A 1, " + textBoxSetAcc.Text.ToString() + " \n");

                //Close the Session
                mbSession.Dispose();
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
            //GPIB Connection string
            string sAddress = "GPIB0::1::INSTR";

            //The VNA uses a message based session
            MessageBasedSession mbSession = null;
            //Open a generic Session first
            Session mySession = null;
            try
            {
                //open a Session to the VNA
                mySession = ResourceManager.GetLocalManager().Open(sAddress);
                //cast this to a message based session
                mbSession = (MessageBasedSession)mySession;

                //Return to Local Control
//                mbSession.Write("RTL\n");

                // Toggle the hardware GPIB REN line.
                GpibSession gpib = (GpibSession)mySession;
                gpib.ControlRen(RenMode.DeassertAfterGtl);


                //Close the Session
                mbSession.Dispose();
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
            //GPIB Connection string
            string sAddress = "GPIB0::1::INSTR";

            //The VNA uses a message based session
            MessageBasedSession mbSession = null;
            //Open a generic Session first
            Session mySession = null;

            try
            {
                //open a Session to the VNA
                mySession = ResourceManager.GetLocalManager().Open(sAddress);
                //cast this to a message based session
                mbSession = (MessageBasedSession)mySession;
                //Set Magnitude, Frequency and Phase for sinusoidal input (default channel 1)
                mbSession.Write(":D:O 1, " + textBoxSetMagn.Text.ToString() + ", " + textBoxSetFreq.Text.ToString() + ", " +  textBoxSetPhase.Text.ToString() + " \n");

                //Close the Session
                mbSession.Dispose();
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
