namespace Minesweeper
{
    partial class Minesweeper
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.pbCanvas = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNbMines = new System.Windows.Forms.Label();
            this.pbPlayer1 = new System.Windows.Forms.PictureBox();
            this.lblPlayerState = new System.Windows.Forms.Label();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.buttonReplay = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.TextBoxPlayerName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlayer1)).BeginInit();
            this.SuspendLayout();
            // 
            // pbCanvas
            // 
            this.pbCanvas.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pbCanvas.Location = new System.Drawing.Point(10, 11);
            this.pbCanvas.Margin = new System.Windows.Forms.Padding(0);
            this.pbCanvas.Name = "pbCanvas";
            this.pbCanvas.Size = new System.Drawing.Size(480, 520);
            this.pbCanvas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCanvas.TabIndex = 0;
            this.pbCanvas.TabStop = false;
            this.pbCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pbCanvas_Paint);
            this.pbCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(507, 144);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 26);
            this.label1.TabIndex = 402;
            this.label1.Text = "Score :";
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore.Location = new System.Drawing.Point(586, 142);
            this.lblScore.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(0, 26);
            this.lblScore.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(507, 180);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 26);
            this.label2.TabIndex = 3;
            this.label2.Text = "Mines :";
            // 
            // lblNbMines
            // 
            this.lblNbMines.AutoSize = true;
            this.lblNbMines.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNbMines.Location = new System.Drawing.Point(586, 178);
            this.lblNbMines.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNbMines.Name = "lblNbMines";
            this.lblNbMines.Size = new System.Drawing.Size(0, 26);
            this.lblNbMines.TabIndex = 4;
            // 
            // pbPlayer1
            // 
            this.pbPlayer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.pbPlayer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbPlayer1.Location = new System.Drawing.Point(502, 11);
            this.pbPlayer1.Margin = new System.Windows.Forms.Padding(2);
            this.pbPlayer1.Name = "pbPlayer1";
            this.pbPlayer1.Size = new System.Drawing.Size(186, 241);
            this.pbPlayer1.TabIndex = 6;
            this.pbPlayer1.TabStop = false;
            // 
            // lblPlayerState
            // 
            this.lblPlayerState.AutoSize = true;
            this.lblPlayerState.BackColor = System.Drawing.Color.Transparent;
            this.lblPlayerState.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblPlayerState.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.lblPlayerState.ForeColor = System.Drawing.Color.Green;
            this.lblPlayerState.Location = new System.Drawing.Point(565, 215);
            this.lblPlayerState.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPlayerState.Name = "lblPlayerState";
            this.lblPlayerState.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblPlayerState.Size = new System.Drawing.Size(75, 26);
            this.lblPlayerState.TabIndex = 403;
            this.lblPlayerState.Text = "ALIVE";
            this.lblPlayerState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonPlay
            // 
            this.buttonPlay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonPlay.Location = new System.Drawing.Point(502, 263);
            this.buttonPlay.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(185, 28);
            this.buttonPlay.TabIndex = 404;
            this.buttonPlay.Text = "PLAY GAME";
            this.buttonPlay.UseVisualStyleBackColor = false;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // buttonReplay
            // 
            this.buttonReplay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonReplay.Location = new System.Drawing.Point(502, 306);
            this.buttonReplay.Margin = new System.Windows.Forms.Padding(2);
            this.buttonReplay.Name = "buttonReplay";
            this.buttonReplay.Size = new System.Drawing.Size(185, 28);
            this.buttonReplay.TabIndex = 405;
            this.buttonReplay.Text = "REPLAY";
            this.buttonReplay.UseVisualStyleBackColor = false;
            this.buttonReplay.Click += new System.EventHandler(this.buttonReplay_Click);
            // 
            // TextBoxPlayerName
            // 
            this.TextBoxPlayerName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.TextBoxPlayerName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextBoxPlayerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.TextBoxPlayerName.Location = new System.Drawing.Point(546, 38);
            this.TextBoxPlayerName.Name = "TextBoxPlayerName";
            this.TextBoxPlayerName.Size = new System.Drawing.Size(100, 31);
            this.TextBoxPlayerName.TabIndex = 406;
            this.TextBoxPlayerName.Text = "Player1";
            this.TextBoxPlayerName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TextBoxPlayerName.SelectionStart = 0;
            // 
            // Minesweeper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(700, 539);
            this.Controls.Add(this.TextBoxPlayerName);
            this.Controls.Add(this.buttonReplay);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.lblPlayerState);
            this.Controls.Add(this.pbPlayer1);
            this.Controls.Add(this.lblNbMines);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbCanvas);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Minesweeper";
            this.Text = "MineSweeper C#";
            this.Load += new System.EventHandler(this.pbCanvas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlayer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblNbMines;
        public System.Windows.Forms.PictureBox pbCanvas;
        private System.Windows.Forms.PictureBox pbPlayer1;
        private System.Windows.Forms.Label lblPlayerState;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Button buttonReplay;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox TextBoxPlayerName;
    }
}

