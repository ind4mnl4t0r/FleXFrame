using FleXFrame.UtilityHub.WinForms.Interfaces;

namespace FleXFrame.UtilityHub.WinForms.Services
{
    public class FormFactory
    {
        /// <summary>
        /// Opens a form inside the specified SplitContainer's right panel.
        /// </summary>
        /// <typeparam name="T">The type of form to open.</typeparam>
        /// <param name="splitContainer">The SplitContainer whose right panel will host the form.</param>
        public static void OpenFormInPanel<T>(SplitContainer splitContainer) where T : Form, new()
        {
            // Check if a form of type T is already open in the right panel
            foreach (Control ctrl in splitContainer.Panel2.Controls)
            {
                if (ctrl is T) // If the form is already open, don't open it again
                {
                    return;
                }
            }

            // Create the new form
            T form = new T();

            // Optional: Dock the form to fill the right panel, so it resizes with the SplitContainer
            form.TopLevel = false;  // This ensures the form is hosted as a child control
            form.Dock = DockStyle.Fill;  // Fill the available space in Panel2

            // Clear any existing controls in Panel2
            splitContainer.Panel2.Controls.Clear();

            // Add the form to the right panel of the SplitContainer
            splitContainer.Panel2.Controls.Add(form);

            // Show the form
            form.Show();
        }

        /// <summary>
        /// Opens a form and optionally passes data to it. 
        /// Allows opening the form as a dialog if specified.
        /// Optionally closes the current form before opening the new form.
        /// </summary>
        /// <typeparam name="T">The type of form to open.</typeparam>
        /// <param name="data">Optional data to pass to the form.</param>
        /// <param name="showAsDialog">Whether to show the form as a dialog.</param>
        /// <param name="closeCurrentForm">Whether to close the current form before opening the new form.</param>
        public static void OpenForm<T>(object? data = null, bool showAsDialog = false, bool closeCurrentForm = false) where T : Form, new()
        {
            // Check if the form is already open (only applies to non-dialog forms)
            if (!showAsDialog)
            {
                Form? existingForm = Application.OpenForms[typeof(T).Name];
                if (existingForm != null)
                {
                    existingForm.BringToFront();
                    return;
                }
            }

            // If specified, close the current form (close the form that called OpenForm)
            if (closeCurrentForm)
            {
                var currentForm = Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.Focused);
                currentForm?.Close();
            }

            // Create a new instance of the form
            T form = new T();

            // If data is provided, pass it to the form if it implements IFormDataReceiver
            if (data != null && form is IFormDataReceiver formWithData)
            {
                formWithData.SendFormData(data);
            }

            // Show as a dialog or non-dialog based on the showAsDialog parameter
            if (showAsDialog)
            {
                form.ShowDialog();
            }
            else
            {
                form.Show();
            }
        }



    }

}
