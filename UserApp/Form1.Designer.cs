namespace UserApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            neardriver = new Button();
            BookingConfirmed = new Button();
            CancelBooking = new Button();
            txtLog = new RichTextBox();
            connectsignal = new Button();
            Initialize = new Button();
            button1 = new Button();
            SendMsg = new Button();
            messagebox = new TextBox();
            SuspendLayout();
            // 
            // neardriver
            // 
            neardriver.Location = new Point(122, 31);
            neardriver.Name = "neardriver";
            neardriver.Size = new Size(94, 29);
            neardriver.TabIndex = 0;
            neardriver.Text = "near Driver";
            neardriver.UseVisualStyleBackColor = true;
            neardriver.Click += neardriver_Click;
            // 
            // BookingConfirmed
            // 
            BookingConfirmed.Location = new Point(122, 139);
            BookingConfirmed.Name = "BookingConfirmed";
            BookingConfirmed.Size = new Size(178, 29);
            BookingConfirmed.TabIndex = 1;
            BookingConfirmed.Text = "BookingConfirmed";
            BookingConfirmed.UseVisualStyleBackColor = true;
            BookingConfirmed.Click += BookingConfirmed_Click;
            // 
            // CancelBooking
            // 
            CancelBooking.Location = new Point(129, 183);
            CancelBooking.Name = "CancelBooking";
            CancelBooking.Size = new Size(171, 29);
            CancelBooking.TabIndex = 2;
            CancelBooking.Text = "Cancel Boking";
            CancelBooking.TextAlign = ContentAlignment.TopCenter;
            CancelBooking.UseVisualStyleBackColor = true;
            CancelBooking.Click += cancelbooking_Click;
            // 
            // txtLog
            // 
            txtLog.Location = new Point(238, 308);
            txtLog.Name = "txtLog";
            txtLog.Size = new Size(415, 106);
            txtLog.TabIndex = 3;
            txtLog.Text = "";
            // 
            // connectsignal
            // 
            connectsignal.Location = new Point(122, 81);
            connectsignal.Name = "connectsignal";
            connectsignal.Size = new Size(161, 29);
            connectsignal.TabIndex = 4;
            connectsignal.Text = "connectsignal";
            connectsignal.UseVisualStyleBackColor = true;
            connectsignal.Click += connectsignal_Click;
            // 
            // Initialize
            // 
            Initialize.Location = new Point(302, 18);
            Initialize.Name = "Initialize";
            Initialize.Size = new Size(94, 29);
            Initialize.TabIndex = 5;
            Initialize.Text = "Initialize";
            Initialize.UseVisualStyleBackColor = true;
            Initialize.Click += Initialize_Click;
            // 
            // button1
            // 
            button1.Location = new Point(160, 235);
            button1.Name = "button1";
            button1.Size = new Size(211, 29);
            button1.TabIndex = 6;
            button1.Text = "Cancel Ride";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // SendMsg
            // 
            SendMsg.Location = new Point(505, 160);
            SendMsg.Name = "SendMsg";
            SendMsg.Size = new Size(158, 29);
            SendMsg.TabIndex = 7;
            SendMsg.Text = "SendMsg";
            SendMsg.UseVisualStyleBackColor = true;
            SendMsg.Click += SendMsg_Click;
            // 
            // messagebox
            // 
            messagebox.Location = new Point(510, 99);
            messagebox.Name = "messagebox";
            messagebox.Size = new Size(153, 27);
            messagebox.TabIndex = 8;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(messagebox);
            Controls.Add(SendMsg);
            Controls.Add(button1);
            Controls.Add(Initialize);
            Controls.Add(connectsignal);
            Controls.Add(txtLog);
            Controls.Add(CancelBooking);
            Controls.Add(BookingConfirmed);
            Controls.Add(neardriver);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button neardriver;
        private Button BookingConfirmed;
        private Button CancelBooking;
        private RichTextBox txtLog;
        private Button connectsignal;
        private Button Initialize;
        private Button button1;
        private Button SendMsg;
        private TextBox messagebox;
    }
}
