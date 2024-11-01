using System;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace LambdaAFR
{
    public partial class Main : Form
    {
        public Main()
        {
            try
            {
                InitializeComponent();
                LambdaNumericUpDown.Minimum = 0.5m;
                LambdaNumericUpDown.Maximum = 2.0m;
                AFRNumericUpDown2.Minimum = 5.0m;
                AFRNumericUpDown2.Maximum = 25.0m;
                CommandedAFRNumericUpDown.Minimum = 5.0m;
                CommandedAFRNumericUpDown.Maximum = 25.0m;
                ActualAFRNumericUpDown.Minimum = 5.0m;
                ActualAFRNumericUpDown.Maximum = 25.0m;
                StoichRatioNumericUpDown.Minimum = 10.0m;
                StoichRatioNumericUpDown.Maximum = 20.0m;
            }
            catch
            {

            }
        }

        private void LambdaNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (sender is NumericUpDown lambdaControl)
                {
                    decimal lambdaValue = lambdaControl.Value;
                    decimal stoichRatio = StoichRatioNumericUpDown.Value;
                    decimal afrValue = lambdaValue * stoichRatio;
                    AFRNumericUpDown2.ValueChanged -= AFRNumericUpDown2_ValueChanged;
                    AFRNumericUpDown2.Value = afrValue;
                    AFRNumericUpDown2.ValueChanged += AFRNumericUpDown2_ValueChanged;
                }
            }
            catch
            {

            }
        }

        private void AFRNumericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (sender is NumericUpDown afrControl)
                {
                    decimal afrValue = afrControl.Value;
                    decimal stoichRatio = StoichRatioNumericUpDown.Value;
                    decimal lambdaValue = afrValue / stoichRatio;
                    LambdaNumericUpDown.ValueChanged -= LambdaNumericUpDown_ValueChanged;
                    LambdaNumericUpDown.Value = lambdaValue;
                    LambdaNumericUpDown.ValueChanged += LambdaNumericUpDown_ValueChanged;
                }
            }
            catch
            {

            }
        }

        private void UpdateFactors()
        {
            try
            {
                decimal stoichRatio = StoichRatioNumericUpDown.Value;
                decimal commandedAFR = CommandedAFRNumericUpDown.Value;
                decimal actualAFR = ActualAFRNumericUpDown.Value;
                if (commandedAFR != 0)
                {
                    decimal correctionFactor = actualAFR / commandedAFR;
                    label2.Text = $"Correction Factor: {correctionFactor:F2}";
                    decimal fuelPercentage = (1 - correctionFactor) * 100;
                    if (fuelPercentage == 0)
                    {
                        label3.Text = $"Fuel Adjustment: {Math.Abs(fuelPercentage):F2}% (Perfect)";
                    }
                    else
                    {
                        if (fuelPercentage > 0)
                        {
                            label3.Text = $"Fuel Adjustment: -{fuelPercentage:F2}% (Remove Fuel)";
                        }
                        else
                        {
                            label3.Text = $"Fuel Adjustment: +{Math.Abs(fuelPercentage):F2}% (Add Fuel)";
                        }
                    }
                    SetLabelColor(actualAFR, commandedAFR, stoichRatio);
                }
                else
                {
                    label2.Text = "Correction Factor: N/A";
                    label3.Text = "Fuel Adjustment: N/A";
                    label3.ForeColor = Color.Black;
                }
            }
            catch
            {

            }
        }

        private void SetLabelColor(decimal actualAFR, decimal commandedAFR, decimal stoichRatio)
        {
            try
            {
                decimal afrDifference = Math.Abs(actualAFR - commandedAFR);
                decimal safeThreshold = stoichRatio * 0.1m;
                decimal cautionThreshold = stoichRatio * 0.2m;
                decimal dangerThreshold = stoichRatio * 0.3m;
                if (afrDifference <= safeThreshold)
                {
                    label3.ForeColor = Color.White;
                }
                else if (afrDifference <= cautionThreshold)
                {
                    label3.ForeColor = Color.Orange;
                }
                else if (afrDifference > dangerThreshold)
                {
                    label3.ForeColor = Color.Red;
                }
            }
            catch
            {

            }
        }

        private void CommandedAFRNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                UpdateFactors();
            }
            catch
            {

            }
        }

        private void ActualAFRNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                UpdateFactors();
            }
            catch
            {

            }
        }

        private void StoichRatioNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                UpdateFactors();
            }
            catch
            {

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://trentstuning.com/");
            }
            catch
            {

            }
        }
    }
}
