using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeatExchangerSimulator
{
    public partial class Simulation : Form
    {
        public Simulation()
        {
            InitializeComponent();
        }





        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                //Logic behind the Heat transfer from the hot fluid Qh

                double hIn = double.Parse(txtHotIn.Text.ToString()); // variable for inlet temperature of hot fluid
                double hOut = double.Parse(txtHotOut.Text.ToString()); //variable for outlet temperature of hot fluid
                double hotFr = double.Parse(fHot.Text.ToString()); // Variable for mass flowrate of hot fluid
                double CpHot = double.Parse(txtCpHot.Text.ToString()); // variable for specific heat capacity of hot fluid
                double Qh = Math.Abs(hotFr * CpHot * (hIn - hOut)); // Qh, varaiable for heat transfer from hot fluid
                lblQh.Text = Qh + " Watt".ToString();  // Label displaying the answer after the logic has been performed 

                //Logic behind Heat Transfer from the cold fluid Qc

                double cIn = double.Parse(txtColdIn.Text.ToString()); // variable for inlet temperature of cold fluid
                double cOut = double.Parse(txtColdOut.Text.ToString()); //variable for outlet temperature of cold fluid
                double CpCold = double.Parse(txtCpCold.Text.ToString()); // variable for specific heat capacity of cold fluid
                double coldFr = double.Parse(fCold.Text.ToString()); // Variable for mass flowrate of hot fluid
                double Qc = Math.Abs(coldFr * CpCold * (cOut - cIn)); // Qc, varaiable for heat transfer from hot fluid
                lblQc.Text = Qc + " Watt".ToString(); // Label displaying the answer after the logic has been performed 

                //logic behind effectiveness
                double Eff;
                if (Qc < Qh)
                {
                    Eff = Qh / Qc;
                }
                else
                {
                    Eff = Qh / Qh;
                }
                lblE.Text = Eff.ToString();

                // Logic behind Qo

                double Qo = Math.Abs((Qh + Qc)) / 2;
                lblQo.Text = Qo + " Watt".ToString();



                // logic behind if flow is cocurrent

                if (checkBox2.Checked == true)
                {
                    // Logic behind L.M.T.D (for co-current flow)
                    double dTt1 = hIn - cIn;
                    double dTt2 = hOut - cOut;
                    double cLMTD = Math.Round((Math.Abs(dTt1 - dTt2)) / (Math.Abs((Math.Log(dTt1 / dTt2))) / (Math.Log(2.71828))), 2);
                    lblLMTD.Text = cLMTD.ToString();
                    // Logic behind cUo
                    double D = double.Parse(txtDiamater.Text.ToString());
                    double noTubes = double.Parse(txtNtubes.Text.ToString());
                    double tLength = double.Parse(txtTubeL.Text.ToString());
                    double Area = Math.Round((noTubes * Math.PI * D * tLength), 2);
                    lblArea.Text = Area.ToString() + " m^2";
                    double cUo = Math.Round(Qo / (Area * cLMTD), 2);
                    lblUo.Text = Text = cUo.ToString() + "W/(M^2.K)";
                }
                else
                {
                    // logic behind L.M.T.D (for counter current flow)
                    double dT1 = hIn - cOut; // At one end of the heat exchanger hot fluid enters and cold fluid leaves, therefore dT1 is the difference between the hot fluid coming in and the cold fluid going out
                    double dT2 = hOut - cIn;
                    double LMTD = Math.Round((Math.Abs(dT1 - dT2)) / (Math.Abs((Math.Log(dT1 / dT2))) / (Math.Log(2.71828))), 2);
                    lblLMTD.Text = LMTD.ToString();

                    //logic behind Uo
                    double D = double.Parse(txtDiamater.Text.ToString());
                    double noTubes = double.Parse(txtNtubes.Text.ToString());
                    double tLength = double.Parse(txtTubeL.Text.ToString());
                    double Area = Math.Round((noTubes * Math.PI * D * tLength), 2);
                    lblArea.Text = Area.ToString() + "m^2";
                    double Uo = Math.Round(Qo / (Area * LMTD), 2);
                    lblUo.Text = Uo.ToString() + " W/(M^2.K)";

                }




            }
            catch (FormatException)
            {

                MessageBox.Show("Please fill the entries completely and ensure that they are filled with numbers only!");
            }
        }



        public void btnReset_Click(object sender, EventArgs e)
        {
            txtHotIn.Text = "";
            txtColdIn.Text = "";
            txtColdOut.Text = "";
            txtCpCold.Text = "";
            txtCpHot.Text = "";
            txtDiamater.Text = "";
            txtHotIn.Text = "";
            txtHotOut.Text = "";
            txtNtubes.Text = "";
            txtTubeL.Text = "";
            fHot.Text = "";
            fCold.Text = "";
            lblArea.Text = "";
            lblE.Text = "";
            lblLMTD.Text = "";
            lblQc.Text = "";
            lblQh.Text = "";
            lblQo.Text = "";
            lblUo.Text = "";
            
        }
    }
}
