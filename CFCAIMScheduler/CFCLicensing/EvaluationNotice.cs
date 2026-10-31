using CFCLicensing.Object;
using System;
using System.Windows.Forms;

namespace CFCLicensing
{
	public partial class EvaluationNotice : Form
	{
		public EvaluationNotice()
		{
			InitializeComponent();
		}
        public void EvaluationNotice_Activated(object sender, System.EventArgs e)
		{
            int daysRemaining = Activation.GetTrialDaysLeft();
			string day = "days";

			if (daysRemaining < 2)
			{
				day = "day";
			}

			lblDaysRemaining.Text = String.Format("{0} {1} Remaining in Evaluation Period", daysRemaining.ToString(), day);
		}

		private void EvaluationNotice_Load(object sender, EventArgs e)
		{
			lblEvaluationPeriod.Text = Properties.Settings.Default.EvaluationPeriod;
			lblSoftWarning.Text = Properties.Settings.Default.SoftwareWarning;
			lblProgramName.Text = Activation.GetProgramName();
			Console.Beep();
		}

		private void cmdAccept_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void cmdConvert_Click(object sender, EventArgs e)
		{
			AuthorizationCode authorizationCode = new AuthorizationCode();
			EvaluationPeriodExpired evaluationPeriodExpired = new EvaluationPeriodExpired();

			Activation.IsTrialExpired = false;
			this.Hide();

			evaluationPeriodExpired.ShowDialog();
			if (Activation.IsProductLicensed == true  || Activation.IsTrialExtended == true)
			{
				Close();
			}
			else
			{
				this.Show();
			}
		}

		private void cmdOrderInformation_Click(object sender, EventArgs e)
		{
			OrderInformation orderInformation = new OrderInformation();

			this.Hide();
			orderInformation.ShowDialog();
			this.Show();
		}

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
	}
}
