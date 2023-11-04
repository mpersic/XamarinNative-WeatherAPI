using CoreGraphics;
using UIKit;

public class CustomTableViewCell : UITableViewCell
{
    public UIImageView CustomImageView { get; set; }
    public UILabel TitleLabel { get; set; }
    public UILabel SubtitleLabel { get; set; }

    public CustomTableViewCell(string reuseIdentifier) : base(UITableViewCellStyle.Default, reuseIdentifier)
    {
        CustomImageView = new UIImageView(new CGRect(10, 10, 40, 40));
        TitleLabel = new UILabel(new CGRect(60, 10, 200, 20));
        SubtitleLabel = new UILabel(new CGRect(60, 30, 200, 20));

        ContentView.AddSubviews(CustomImageView, TitleLabel, SubtitleLabel);
    }
}
