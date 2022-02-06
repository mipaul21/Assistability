/// <summary>
/// Your Name: Whitney Vinson
/// Created: 2021/04/15
/// 
/// The page to add, view or edit 
/// Performance Event details.
/// </summary>
///
/// <remarks>
/// 
/// </remarks>
using DataStorageModels;
using LogicLayer;
using LogicLayerInterfaces;
using PresentationHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PresentationWPF
{
    /// <summary>
    /// Interaction logic for pgAddEditViewPerformanceEvent.xaml
    /// </summary>
    public partial class pgAddEditViewPerformanceEvent : Page
    {
        private PerformanceEvent _performanceEvent;
        private bool _addEvent = false;
        private IPerformanceEventManager _performanceEventManager = new PerformanceEventManager();

        public pgAddEditViewPerformanceEvent()
        {
            _performanceEvent = new PerformanceEvent();
            _addEvent = true;

            InitializeComponent();
        }

        public pgAddEditViewPerformanceEvent(PerformanceEvent performanceEvent)
        {
            _performanceEvent = performanceEvent;

            InitializeComponent();
        }
        /// <summary>
        /// Your Name: Whitney Vinson
        /// Created: 2021/04/15
        /// 
        /// Saving an edited 
        /// Performance Event.
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// 
        /// </remarks>
        private void btnEditSave_Click(object sender, RoutedEventArgs e)
        {
            if (((string)btnEditSave.Content) == "Edit")
            {
                setupEdit();
            }
            else
            {
                if (_addEvent == false)
                {

                    if (!txtEventDescription.Text.IsValidEventDescription())
                    {
                        MessageBox.Show("Invalid Event Description.");
                        txtEventDescription.Focus();
                        txtEventDescription.SelectAll();
                        return;
                    }
                    if (!txtEventResult.Text.IsValidEventResult())
                    {
                        MessageBox.Show("Invalid Event Result.");
                        txtEventResult.Focus();
                        txtEventResult.SelectAll();
                        return;
                    }
                    var newPerformanceEvent = new PerformanceEvent()
                    {
                        EventDescription = txtEventResult.Text,
                        EventResult = txtEventResult.Text,
                        EventEditDate = DateTime.Now
                    };

                    try
                    {
                        _performanceEventManager.EditPerformanceEvent(_performanceEvent, newPerformanceEvent);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    }
                }
                else
                {
                    if (!txtEventDescription.Text.IsValidEventDescription())
                    {
                        MessageBox.Show("Invalid Event Description.");
                        txtEventDescription.Focus();
                        txtEventDescription.SelectAll();
                        return;
                    }
                    if (!txtEventResult.Text.IsValidEventResult())
                    {
                        MessageBox.Show("Invalid Event Result.");
                        txtEventResult.Focus();
                        txtEventResult.SelectAll();
                        return;
                    }

                    var newPerformanceEvent = new PerformanceEvent()
                    {
                        DateOfOccurance = DateTime.Now,
                        EventDescription = txtEventResult.Text,
                        EventResult = txtEventResult.Text,
                    };

                    try
                    {
                        _performanceEventManager.AddNewPerformanceEvent(newPerformanceEvent.PerformanceName,
                            newPerformanceEvent.UserIDClient, newPerformanceEvent.UserIDReporter, newPerformanceEvent);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    }
                }
            }
        }
        /// <summary>
        /// Your Name: Whitney Vinson
        /// Created: 2021/04/15
        /// 
        /// The page load event.
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// 
        /// </remarks>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (_addEvent)
            {
                txtDateOfOccurance.Text = DateTime.Now.ToShortDateString();
                txtDateOfOccurance.IsEnabled = false;
                txtEventDescription.Text = "";
                txtEventResult.Text = "";
                setupEdit();
            }
            else 
            {
                txtDateOfOccurance.Text = _performanceEvent.DateOfOccurance.ToShortDateString();
                txtEventDescription.Text = _performanceEvent.EventDescription;
                txtEventResult.Text = _performanceEvent.EventResult;
            }
        }
        /// <summary>
        /// Your Name: Whitney Vinson
        /// Created: 2021/04/15
        /// 
        /// The save button for Performance Event.
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// 
        /// </remarks>
        private void setupEdit()
        {
            btnEditSave.Content = "Save";
            txtDateOfOccurance.IsReadOnly = true;
            txtEventDescription.IsReadOnly = false;
            txtEventResult.IsReadOnly = false;
            txtEventDescription.BorderBrush = Brushes.Black;
            txtEventDescription.BorderBrush = Brushes.Black;
            txtEventDescription.Focus();
        }
        /// <summary>
        /// Your Name: Whitney Vinson
        /// Created: 2021/04/15
        /// 
        /// The cancel button, to return
        /// to previous page.
        /// </summary>
        ///
        /// <remarks>
        /// 
        /// </remarks>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
