// *****************************************************************************
// 
//  (c) Crownwood Software Ltd 2004-2005. All rights reserved. 
//	The software and associated documentation supplied hereunder are the 
//	proprietary information of Crownwood Software Ltd, Bracknell, 
//	Berkshire, England and are supplied subject to licence terms.
// 
//  Version 3.0.3.0 	www.crownwood.net
// *****************************************************************************

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Crownwood.DotNetMagic.Common;
using Crownwood.DotNetMagic.Controls;

namespace Crownwood.DotNetMagic.Forms
{
	/// <summary>
	/// Summary description for WizardDialog.
	/// </summary>
	public class WizardDialog : System.Windows.Forms.Form
	{
		/// <summary>
		/// Specifies how the title is generated.
		/// </summary>
	    public enum TitleModes
	    {
			/// <summary>
			/// Specifies the Form.Text be used as the title.
			/// </summary>
	        None,

			/// <summary>
			/// Specifies the wizard page title is appended.
			/// </summary>
	        WizardPageTitle,
			
			/// <summary>
			/// Specifies the wizard page sub-title is appended.
			/// </summary>
	        WizardPageSubTitle,

			/// <summary>
			/// Specifies the wizard page caption title is appended.
			/// </summary>
	        WizardPageCaptionTitle,

			/// <summary>
			/// Specifies a step text be generated and appended
			/// </summary>
	        Steps
	    }

		/// <summary>
		/// Exposed control for direct access in designer
		/// </summary>
		public WizardControl wizardControl;
	
		// Instance fields
	    private string _cachedTitle;
	    private TitleModes _titleMode;
	
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Initializes a new instance of the WizardDialog class.
		/// </summary>
		public WizardDialog()
		{
			// Required for Windows Form Designer support
			InitializeComponent();

            // Initialize properties
            ResetTitleMode();
            
            // Hook into wizard page collection changes
            wizardControl.WizardPages.Cleared += new CollectionClear(OnPagesCleared);
            wizardControl.WizardPages.Inserted += new CollectionChange(OnPagesChanged);
            wizardControl.WizardPages.Removed += new CollectionChange(OnPagesChanged);
        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(WizardDialog));
			this.wizardControl = new Crownwood.DotNetMagic.Controls.WizardControl();
			this.SuspendLayout();
			// 
			// wizardControl
			// 
			this.wizardControl.Controls.Add(this.wizardControl.HeaderPanel);
			this.wizardControl.Controls.Add(this.wizardControl.TrailerPanel);
			this.wizardControl.Dock = System.Windows.Forms.DockStyle.Fill;
			// 
			// wizardControl.HeaderPanel
			// 
			this.wizardControl.HeaderPanel.BackColor = System.Drawing.SystemColors.Window;
			this.wizardControl.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.wizardControl.HeaderPanel.Location = new System.Drawing.Point(0, 0);
			this.wizardControl.HeaderPanel.Name = "_panelTop";
			this.wizardControl.HeaderPanel.Size = new System.Drawing.Size(416, 80);
			this.wizardControl.HeaderPanel.TabIndex = 1;
			this.wizardControl.Location = new System.Drawing.Point(0, 0);
			this.wizardControl.Name = "wizardControl";
			this.wizardControl.Picture = ((System.Drawing.Image)(resources.GetObject("wizardControl.Picture")));
			this.wizardControl.SelectedIndex = -1;
			this.wizardControl.Size = new System.Drawing.Size(416, 285);
			this.wizardControl.TabIndex = 0;
			// 
			// wizardControl.TrailerPanel
			// 
			this.wizardControl.TrailerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.wizardControl.TrailerPanel.Location = new System.Drawing.Point(0, 237);
			this.wizardControl.TrailerPanel.Name = "_panelBottom";
			this.wizardControl.TrailerPanel.Size = new System.Drawing.Size(416, 48);
			this.wizardControl.TrailerPanel.TabIndex = 2;
			this.wizardControl.CancelClick += new System.EventHandler(this.OnCancelClick);
			this.wizardControl.CloseClick += new System.EventHandler(this.OnCloseClick);
			this.wizardControl.NextClick += new System.ComponentModel.CancelEventHandler(this.OnNextClick);
			this.wizardControl.FinishClick += new System.EventHandler(this.OnFinishClick);
			this.wizardControl.WizardPageEnter += new Crownwood.DotNetMagic.Controls.WizardControl.WizardPageHandler(this.OnWizardPageEnter);
			this.wizardControl.BackClick += new System.ComponentModel.CancelEventHandler(this.OnBackClick);
			this.wizardControl.HelpClick += new System.EventHandler(this.OnHelpClick);
			this.wizardControl.WizardPageLeave += new Crownwood.DotNetMagic.Controls.WizardControl.WizardPageHandler(this.OnWizardPageLeave);
			this.wizardControl.WizardCaptionTitleChanged += new System.EventHandler(this.OnWizardCaptionTitleChanged);
			this.wizardControl.SelectionChanged += new System.EventHandler(this.OnSelectionChanged);
			this.wizardControl.UpdateClick += new System.EventHandler(this.OnUpdateClick);
			// 
			// WizardDialog
			// 
			this.ClientSize = new System.Drawing.Size(416, 285);
			this.Controls.Add(this.wizardControl);
			this.Name = "WizardDialog";
			this.Text = "Wizard Dialog";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Gets direct access the wizard control.
		/// </summary>
		public WizardControl WizardControl
		{
			get { return wizardControl; }
		}

		/// <summary>
		/// Gets and sets the text using in the dialog title.
		/// </summary>
        public new string Text
        {
            get { return _cachedTitle; }
            
            set 
            {
                // Store the provided value
                _cachedTitle = value;
                
                // Apply the title mode extra to the end
                ApplyTitleMode();
            }
        }
        
		/// <summary>
		/// Gets and set how hte title is automatically generated.
		/// </summary>
        [Category("Wizard")]
        [Description("Determine how the title is automatically defined")]
        [DefaultValue(typeof(TitleModes), "WizardPageCaptionTitle")]
        public TitleModes TitleMode
        {
            get { return _titleMode; }
            
            set
            {
                if (_titleMode != value)
                {
                    _titleMode = value;
                    ApplyTitleMode();
                }
            }
        }

		/// <summary>
		/// Resets the TitleMode property to its default value. 
		/// </summary>
        public void ResetTitleMode()
        {
            TitleMode = TitleModes.WizardPageCaptionTitle;
        }
        
		/// <summary>
		/// Generate the title text.
		/// </summary>
        protected virtual void ApplyTitleMode()
        {
            string newTitle = _cachedTitle;

            // Calculate new title text
            switch(_titleMode)
            {
                case TitleModes.None:
                    // Do nothing!
                    break;
                case TitleModes.Steps:
                    // Get the current page
                    int selectedPage = wizardControl.SelectedIndex;
                    int totalPages = wizardControl.WizardPages.Count;

                    // Only need separator if some text is already present                    
                    if (newTitle.Length > 0)
                        newTitle += " - ";
                    
                    // Append the required text
                    newTitle += "Step " + (selectedPage + 1).ToString() + " of " + totalPages.ToString();
                    break;
                case TitleModes.WizardPageTitle:
                    // Do we have a valid page currently selected?
                    if (wizardControl.SelectedIndex != -1)
                    {
                        // Get the page
                        WizardPage wp = wizardControl.WizardPages[wizardControl.SelectedIndex];

                        // Only need separator if some text is already present                    
                        if (newTitle.Length > 0)
                            newTitle += " - ";
                        
                        // Append page title to the title
                        newTitle += wp.Title;
                    }
                    break;
                case TitleModes.WizardPageSubTitle:
                    // Do we have a valid page currently selected?
                    if (wizardControl.SelectedIndex != -1)
                    {
                        // Get the page
                        WizardPage wp = wizardControl.WizardPages[wizardControl.SelectedIndex];
                        
                        // Only need separator if some text is already present                    
                        if (newTitle.Length > 0)
                            newTitle += " - ";

                        // Append page sub-title to the title
                        newTitle += wp.SubTitle;
                    }
                    break;
                case TitleModes.WizardPageCaptionTitle:
                    // Do we have a valid page currently selected?
                    if (wizardControl.SelectedIndex != -1)
                    {
                        // Get the page
                        WizardPage wp = wizardControl.WizardPages[wizardControl.SelectedIndex];
                        
                        // Only need separator if some text is already present                    
                        if (newTitle.Length > 0)
                            newTitle += " - ";

                        // Append page sub-title to the title
                        newTitle += wp.CaptionTitle;
                    }
                    break;
            }
            
            // Use base class to update actual caption bar
            base.Text = newTitle;
        }
        
		/// <summary>
		/// Occurs when wizard pages are cleared.
		/// </summary>
        protected virtual void OnPagesCleared()
        {
            // Update the caption bar to reflect change in selection
            ApplyTitleMode();
        }
        
		/// <summary>
		/// Occurs when a wizard page is added or removed.
		/// </summary>
		/// <param name="index">Index into wizard page collection.</param>
		/// <param name="value">WizardPage instance reference.</param>
        protected virtual void OnPagesChanged(int index, object value)
        {
            // Update the caption bar to reflect change in selection
            ApplyTitleMode();
        }

		/// <summary>
		/// Occurs when the close button is clicked.
		/// </summary>
		/// <param name="sender">CloseButton reference.</param>
		/// <param name="e">An EventArgs structure containing event data.</param>
        protected virtual void OnCloseClick(object sender, EventArgs e)
        {
            // By default we close the Form
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

		/// <summary>
		/// Occurs when the finish button is clicked.
		/// </summary>
		/// <param name="sender">FinishButton reference.</param>
		/// <param name="e">An EventArgs structure containing event data.</param>
        protected virtual void OnFinishClick(object sender, EventArgs e)
        {
            // By default we close the Form
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

		/// <summary>
		/// Occurs when the cancel button is clicked.
		/// </summary>
		/// <param name="sender">CancelButton reference.</param>
		/// <param name="e">An EventArgs structure containing event data.</param>
        protected virtual void OnCancelClick(object sender, EventArgs e)
        {
            // By default we close the Form
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

		/// <summary>
		/// Occurs when the next button is clicked.
		/// </summary>
		/// <param name="sender">NextButton reference.</param>
		/// <param name="e">An EventArgs structure containing event data.</param>
        protected virtual void OnNextClick(object sender, CancelEventArgs e)
        {
            // By default we do nothing, let derived class override
        }

		/// <summary>
		/// Occurs when the back button is clicked.
		/// </summary>
		/// <param name="sender">BackButton reference.</param>
		/// <param name="e">An EventArgs structure containing event data.</param>
        protected virtual void OnBackClick(object sender, CancelEventArgs e)
        {
            // By default we do nothing, let derived class override
        }
    
		/// <summary>
		/// Occurs when the update button is clicked.
		/// </summary>
		/// <param name="sender">UpdateButton reference.</param>
		/// <param name="e">An EventArgs structure containing event data.</param>
        protected virtual void OnUpdateClick(object sender, EventArgs e)
        {
            // By default we do nothing, let derived class override
        }

		/// <summary>
		/// Occurs when the help button is clicked.
		/// </summary>
		/// <param name="sender">HelpButton reference.</param>
		/// <param name="e">An EventArgs structure containing event data.</param>
        protected virtual void OnHelpClick(object sender, EventArgs e)
        {
            // By default we do nothing, let derived class override
        }

		/// <summary>
		/// Occurs when a change in selected wizard page happens.
		/// </summary>
		/// <param name="sender">WizardPageCollection reference.</param>
		/// <param name="e">An EventArgs structure containing event data.</param>
        protected virtual void OnSelectionChanged(object sender, EventArgs e)
        {
            // Update the caption bar to reflect change in selection
            ApplyTitleMode();
        }

		/// <summary>
		/// Occurs when a wizard page is entered.
		/// </summary>
		/// <param name="wp">WizardPage reference.</param>
		/// <param name="wc">WizardControl reference.</param>
        protected virtual void OnWizardPageEnter(Controls.WizardPage wp, Controls.WizardControl wc)
        {
            // By default we do nothing, let derived class override
        }

		/// <summary>
		/// Occurs when leaving a wizard page.
		/// </summary>
		/// <param name="wp">WizardPage reference.</param>
		/// <param name="wc">WizardControl reference.</param>
        protected virtual void OnWizardPageLeave(Controls.WizardPage wp, Controls.WizardControl wc)
        {
            // By default we do nothing, let derived class override
        }

		/// <summary>
		/// Occurs when a wizard page caption title has changed.
		/// </summary>
		/// <param name="sender">WizardPage reference.</param>
		/// <param name="e">An EventArgs structure containing event data.</param>
        protected virtual void OnWizardCaptionTitleChanged(object sender, EventArgs e)
        {
            // Update the caption bar to reflect change in selection
            ApplyTitleMode();
        }
    }
}
