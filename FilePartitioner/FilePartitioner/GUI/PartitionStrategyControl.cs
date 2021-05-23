using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FilePartitioner.GUI
{
    public partial class PartitionStrategyControl : UserControl
    {
        private readonly Dictionary<string, StrategySelection> displayToInternalMap;

        public PartitionStrategyControl()
        {
            InitializeComponent();
            displayToInternalMap = new Dictionary<string, StrategySelection>();
            AddComboItem("Default (fastest)", StrategySelection.Greedy);
            strategyComboBox.SelectedIndex = 0;
        }

        StrategySelection GetStrategySelection()
        {
            string selectedText = strategyComboBox.GetItemText(
                strategyComboBox.SelectedItem);
            return displayToInternalMap[selectedText];
        }

        private void AddComboItem(
            string displayName, StrategySelection internalName)
        {
            strategyComboBox.Items.Add(displayName);
            displayToInternalMap.Add(displayName, internalName);
        }
    }
}
