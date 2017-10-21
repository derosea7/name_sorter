using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace Sorter
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnRun;
    private Label lblResult;

    // If needed, change file paths here.
    private string inputFilePath = "input.txt";
    private string outputFilePath = "output.txt";
    
    // Gets false if exception encountered.
    private bool resultSuccess = true;

    // Contains the final message to display to user.
    private string result = "";

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
      this.btnRun = new System.Windows.Forms.Button();
      this.lblResult = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // btnRun
      // 
      this.btnRun.Location = new System.Drawing.Point(49, 21);
      this.btnRun.Name = "btnRun";
      this.btnRun.Size = new System.Drawing.Size(166, 48);
      this.btnRun.TabIndex = 0;
      this.btnRun.Text = "&Run";
      this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
      // 
      // lblResult
      // 
      this.lblResult.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblResult.Location = new System.Drawing.Point(0, 87);
      this.lblResult.Name = "lblResult";
      this.lblResult.Size = new System.Drawing.Size(264, 66);
      this.lblResult.TabIndex = 1;
      // 
      // Form1
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(264, 153);
      this.Controls.Add(this.lblResult);
      this.Controls.Add(this.btnRun);
      this.Name = "Form1";
      this.Text = "Sorter";
      this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		/// <summary>
		/// Read in text file from bin folder and output a sorted version.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRun_Click(object sender, System.EventArgs e)
		{
      //TODO: Insert code here...

      // Read in names.
      List<string> rawNames = ReadInNames();

      if (resultSuccess != false)
      {
        // Clean names.
        List<Person> people = FormatNames(rawNames);

        // Sort names.
        List<Person> sortedPeople = people.OrderBy(x => x.firstName).ThenBy(x => x.lastName).ToList();

        OutputNames(sortedPeople);

      }

      if (resultSuccess)
      {
        result = "Operation successful.";
      }

      lblResult.Text = result;
    }

    /// <summary>
    /// Given there exists a text file in the bin folder containing
    /// people's names, this method will read each name into a List.
    /// On error, a message will be set to display back to the user.
    /// </summary>
    /// <returns>List of unformatted names.</returns>
    private List<string> ReadInNames()
    {
      List<string> rawNames = new List<string>();
      StreamReader sr = null;
      try
      {
        sr = new StreamReader(inputFilePath);
        while (sr.Peek() >= 0)
        {
          rawNames.Add(sr.ReadLine());
        }
      }
      catch (FileNotFoundException fnf)
      {
        Fail("There was an error locating input.txt." +
          " Make sure the input file is in the bin folder before compiling.");
      }
      catch (Exception ex)
      {
        Fail("There was an error processing input.txt");
      }
      finally
      {
        if (sr != null)
        {
          sr.Dispose();
        }
      }

      return rawNames;
    }

    /// <summary>
    /// Given a list of first and last names seperated by one or more spaces,
    /// this method will return a Person object with a first and last name.
    /// </summary>
    /// <param name="rawNames">List of unformatted names.</param>
    /// <returns>List of Person objs with properly parsed names.</returns>
    private List<Person> FormatNames(List<string> rawNames)
    {
      // Clean names.
      List<Person> people = new List<Person>();
      foreach (string raw in rawNames)
      {
        Person person = new Person();
        string[] cleaned = raw.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        if (cleaned.Length > 0)
        {
          person.firstName = cleaned[0];

          if (cleaned.Length > 1)
          {
            person.lastName = cleaned[1];
          }
          people.Add(person);
        }
      }

      return people;
    }

    // Write names to a text file.
    private void OutputNames(List<Person> peopleList)
    {
      using (StreamWriter writer = new StreamWriter(outputFilePath))
      {
        foreach (Person name in peopleList)
        {
          try
          {
            writer.WriteLine(name.ToString());
          }
          catch (Exception ex)
          {
            Fail("There was an error while attempting to write output.");
          }
        }
      }
    }

    // Set result to failure and append a string message to display to user.
    void Fail(string failureMessage)
    {
      resultSuccess = false;
      result += failureMessage;
    }
	}
}
