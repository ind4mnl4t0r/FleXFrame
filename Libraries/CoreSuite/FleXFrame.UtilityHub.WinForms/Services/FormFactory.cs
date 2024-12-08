using FleXFrame.UtilityHub.WinForms.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FleXFrame.UtilityHub.WinForms.Services
{
    public class FormFactory
    {
        private static IServiceProvider? _serviceProvider;

        // Set the service provider, called once in the main application
        public static void SetServiceProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Opens a form inside the specified SplitContainer's right panel.
        /// </summary>
        /// <typeparam name="T">The type of form to open.</typeparam>
        /// <param name="splitContainer">The SplitContainer whose right panel will host the form.</param>
        public static void OpenFormInPanel<T>(SplitContainer splitContainer) where T : Form
        {
            if (_serviceProvider == null)
            {
                throw new InvalidOperationException("Service provider is not initialized.");
            }

            // Check if the form is already open in Panel2
            foreach (Control ctrl in splitContainer.Panel2.Controls)
            {
                if (ctrl is T)
                {
                    ctrl.BringToFront();
                    return;
                }
            }

            // Resolve the form from the DI container
            var form = _serviceProvider.GetRequiredService<T>();

            // Configure the form to be hosted in the SplitContainer
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;

            // Clear existing controls and add the new form
            splitContainer.Panel2.Controls.Clear();
            splitContainer.Panel2.Controls.Add(form);

            form.Show();
        }

        public static void OpenFormInPanel<T>(SplitContainer splitContainer, Func<T> formFactory) where T : Form
        {
            // Check if the form of type T is already open in Panel2
            var existingForm = splitContainer.Panel2.Controls.OfType<T>().FirstOrDefault();
            if (existingForm != null)
            {
                existingForm.BringToFront();
                return;
            }

            // Create a new instance of the form using the factory
            T form = formFactory();
            form.TopLevel = false;    // Allow the form to be hosted as a child control
            form.Dock = DockStyle.Fill; // Make the form fill the entire Panel2 area

            // Clear any existing controls in Panel2
            splitContainer.Panel2.Controls.Clear();

            // Add the form to Panel2
            splitContainer.Panel2.Controls.Add(form);

            // Display the form
            form.Show();
        }




        /// <summary>
        /// Opens a form and optionally passes data to it. 
        /// Allows opening the form as a dialog if specified.
        /// Optionally closes the current form before opening the new form.
        /// Example FormFactory.OpenForm(() => new DashboardForm());
        /// </summary>
        /// <typeparam name="T">The type of form to open.</typeparam>
        /// <param name="data">Optional data to pass to the form.</param>
        /// <param name="showAsDialog">Whether to show the form as a dialog.</param>
        /// <param name="closeCurrentForm">Whether to close the current form before opening the new form.</param>
        public static void OpenForm<T>(Func<T> formFactory, object? data = null, bool showAsDialog = false, bool closeCurrentForm = false) where T : Form
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

            // Create a new instance of the form using the factory method
            T form = formFactory();

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

        /// <summary>
        /// Example FormFactory.OpenForm(() => new DashboardForm());
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="showAsDialog"></param>
        /// <param name="closeCurrentForm"></param>
        public static void OpenForm<T>(object? data = null, bool showAsDialog = false, bool closeCurrentForm = false) where T : Form, new()
        {
            OpenForm(() => new T(), data, showAsDialog, closeCurrentForm);
        }

        public static DialogResult OpenFormAsDialog<T>() where T : Form
        {
            if (_serviceProvider == null)
            {
                throw new InvalidOperationException("Service provider is not initialized.");
            }

            // Resolve the form from the DI container
            var form = _serviceProvider.GetRequiredService<T>();

            // Show the form as a modal dialog
            return form.ShowDialog();
        }

    }

}
