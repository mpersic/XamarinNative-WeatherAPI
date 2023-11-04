using System;
using Foundation;
using UIKit;

namespace NNChallenge.iOS
{
    public class TableViewDataSource : UITableViewSource
    {
        public TableViewDataSource()
        {
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("cell", indexPath);

            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Subtitle, "cell");
            }

            cell.TextLabel.Text = String.Format("{0}", indexPath.Row);

            return cell;
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return 50;
        }
    }
}
