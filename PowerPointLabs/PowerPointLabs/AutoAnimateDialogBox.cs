﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PowerPointLabs
{
    public partial class AutoAnimateDialogBox : Form
    {
        public delegate void UpdateSettingsDelegate(float animationDuration, bool smoothAnimationChecked);
        public UpdateSettingsDelegate SettingsHandler;
        private float lastDuration;
        public AutoAnimateDialogBox()
        {
            InitializeComponent();
        }

        public AutoAnimateDialogBox(float defaultDuration, bool smoothChecked)
            : this()
        {
            this.textBox1.Text = defaultDuration.ToString("f");
            this.checkBox1.Checked = smoothChecked;
            lastDuration = defaultDuration;
        }

        private void AutoAnimateDialogBox_Load(object sender, EventArgs e)
        {
            ToolTip ttCheckBox = new ToolTip();
            ttCheckBox.SetToolTip(checkBox1, "Use a frame-based approach for smoother resize animations.\nThis may result in larger file sizes and slower loading times for animated slides.");
            ToolTip ttTextField = new ToolTip();
            ttTextField.SetToolTip(textBox1, "The duration (in seconds) for the animations in the animation slides to be created.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void textBox1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            float enteredValue;
            if (float.TryParse(textBox1.Text, out enteredValue))
            {
                if (enteredValue < 0.01)
                {
                    enteredValue = 0.01f;
                }
                else if (enteredValue > 59.0)
                {
                    enteredValue = 59.0f;
                }
            }
            else 
            {
                enteredValue = lastDuration;
            }
            textBox1.Text = enteredValue.ToString("f");
            lastDuration = enteredValue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SettingsHandler(float.Parse(this.textBox1.Text), this.checkBox1.Checked);
            this.Dispose();
        }
    }
}