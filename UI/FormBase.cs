using System;
using System.Windows.Forms;

namespace SyncWhole.UI
{
	public class FormBase : Form
	{
		protected void Dispatch(Action action)
		{
			if (InvokeRequired)
			{
				BeginInvoke(action);
			}
			else
			{
				action.Invoke();
			}
		}

		protected sealed override void OnFormClosing(FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				Hide();
				e.Cancel = true;
			}
			base.OnFormClosing(e);
		}

		public void Display()
		{
			if (!Visible)
			{
				Show();
			}
			else
			{
				Focus();
			}
		}
	}
}