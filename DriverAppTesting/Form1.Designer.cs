namespace DriverAppTesting
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
            Online = new Button();
            acceptrekla = new Button();
            StartPickup = new Button();
            ReachedPickup = new Button();
            StartTrip = new Button();
            EndTrip = new Button();
            cancelRekla = new Button();
            txtLog = new RichTextBox();
            button1 = new Button();
            button2 = new Button();
            Initialize = new Button();
            CancelRide = new Button();
            button3 = new Button();
            messagebox = new TextBox();
            OTPTextBox = new TextBox();
            GetToken = new Button();
            button4 = new Button();
            SuspendLayout();
            // 
            // Online
            // 
            Online.Location = new Point(60, 59);
            Online.Name = "Online";
            Online.Size = new Size(94, 29);
            Online.TabIndex = 0;
            Online.Text = "Online";
            Online.UseVisualStyleBackColor = true;
            Online.Click += Online_Click;
            // 
            // acceptrekla
            // 
            acceptrekla.Location = new Point(60, 104);
            acceptrekla.Name = "acceptrekla";
            acceptrekla.Size = new Size(130, 29);
            acceptrekla.TabIndex = 1;
            acceptrekla.Text = "AcceptRekla";
            acceptrekla.UseVisualStyleBackColor = true;
            acceptrekla.Click += acceptrekla_Click;
            // 
            // StartPickup
            // 
            StartPickup.Location = new Point(60, 199);
            StartPickup.Name = "StartPickup";
            StartPickup.Size = new Size(94, 29);
            StartPickup.TabIndex = 2;
            StartPickup.Text = "StartPickup";
            StartPickup.UseVisualStyleBackColor = true;
            StartPickup.Click += StartPickup_Click;
            // 
            // ReachedPickup
            // 
            ReachedPickup.Location = new Point(271, 40);
            ReachedPickup.Name = "ReachedPickup";
            ReachedPickup.Size = new Size(142, 29);
            ReachedPickup.TabIndex = 3;
            ReachedPickup.Text = "ReachedPickup";
            ReachedPickup.UseVisualStyleBackColor = true;
            ReachedPickup.Click += ReachedPickup_Click;
            // 
            // StartTrip
            // 
            StartTrip.Location = new Point(569, 125);
            StartTrip.Name = "StartTrip";
            StartTrip.Size = new Size(94, 29);
            StartTrip.TabIndex = 4;
            StartTrip.Text = "Start Trip";
            StartTrip.UseVisualStyleBackColor = true;
            StartTrip.Click += StartTrip_Click;
            // 
            // EndTrip
            // 
            EndTrip.Location = new Point(271, 153);
            EndTrip.Name = "EndTrip";
            EndTrip.Size = new Size(94, 29);
            EndTrip.TabIndex = 4;
            EndTrip.Text = "EndTrip";
            EndTrip.UseVisualStyleBackColor = true;
            EndTrip.Click += EndTrip_Click;
            // 
            // cancelRekla
            // 
            cancelRekla.Location = new Point(60, 153);
            cancelRekla.Name = "cancelRekla";
            cancelRekla.Size = new Size(130, 29);
            cancelRekla.TabIndex = 4;
            cancelRekla.Text = "cancelRekla";
            cancelRekla.UseVisualStyleBackColor = true;
            cancelRekla.Click += cancelRekla_Click;
            // 
            // txtLog
            // 
            txtLog.Location = new Point(12, 285);
            txtLog.Name = "txtLog";
            txtLog.Size = new Size(401, 166);
            txtLog.TabIndex = 6;
            txtLog.Text = "";
            // 
            // button1
            // 
            button1.Location = new Point(471, 363);
            button1.Name = "button1";
            button1.Size = new Size(140, 29);
            button1.TabIndex = 7;
            button1.Text = "startlocation";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(644, 363);
            button2.Name = "button2";
            button2.Size = new Size(123, 29);
            button2.TabIndex = 8;
            button2.Text = "stoplocation";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Initialize
            // 
            Initialize.Location = new Point(60, 24);
            Initialize.Name = "Initialize";
            Initialize.Size = new Size(94, 29);
            Initialize.TabIndex = 9;
            Initialize.Text = "Initialize";
            Initialize.UseVisualStyleBackColor = true;
            Initialize.Click += Initialize_Click;
            // 
            // CancelRide
            // 
            CancelRide.Location = new Point(259, 199);
            CancelRide.Name = "CancelRide";
            CancelRide.Size = new Size(142, 29);
            CancelRide.TabIndex = 10;
            CancelRide.Text = "Cancel Ride";
            CancelRide.UseVisualStyleBackColor = true;
            CancelRide.Click += CancelRide_Click;
            // 
            // button3
            // 
            button3.Location = new Point(544, 259);
            button3.Name = "button3";
            button3.Size = new Size(152, 30);
            button3.TabIndex = 11;
            button3.Text = "Sendmessage";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // messagebox
            // 
            messagebox.Location = new Point(544, 211);
            messagebox.Name = "messagebox";
            messagebox.Size = new Size(125, 27);
            messagebox.TabIndex = 12;
            // 
            // OTPTextBox
            // 
            OTPTextBox.Location = new Point(551, 65);
            OTPTextBox.Name = "OTPTextBox";
            OTPTextBox.Size = new Size(125, 27);
            OTPTextBox.TabIndex = 13;
            // 
            // GetToken
            // 
            GetToken.Location = new Point(485, 15);
            GetToken.Name = "GetToken";
            GetToken.Size = new Size(94, 29);
            GetToken.TabIndex = 14;
            GetToken.Text = "GetToken";
            GetToken.UseVisualStyleBackColor = true;
            GetToken.Click += GetToken_Click_1;
            // 
            // button4
            // 
            button4.Location = new Point(265, 97);
            button4.Name = "button4";
            button4.Size = new Size(136, 29);
            button4.TabIndex = 15;
            button4.Text = "GetTrip";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button4);
            Controls.Add(GetToken);
            Controls.Add(OTPTextBox);
            Controls.Add(messagebox);
            Controls.Add(button3);
            Controls.Add(CancelRide);
            Controls.Add(Initialize);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(txtLog);
            Controls.Add(cancelRekla);
            Controls.Add(EndTrip);
            Controls.Add(StartTrip);
            Controls.Add(ReachedPickup);
            Controls.Add(StartPickup);
            Controls.Add(acceptrekla);
            Controls.Add(Online);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Online;
        private Button acceptrekla;
        private Button StartPickup;
        private Button ReachedPickup;
        private Button StartTrip;
        private Button EndTrip;
        private Button cancelRekla;
        private RichTextBox txtLog;
        private Button button1;
        private Button button2;
        private Button Initialize;
        private Button CancelRide;
        private Button button3;
        private TextBox messagebox;
        private TextBox OTPTextBox;
        private Button GetToken;
        private Button button4;
    }
}
