using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CMS2.Client
{
    class Resizer
    {
        public struct ControlInfo
        {
            public string name;
            public string parentName;
            public double leftOffsetPercent;
            public double topOffsetPercent;
            public double heightPercent;
            public int originalHeight;
            public int originalWidth;
            public double widthPercent;
            public Single originalFontSize;
        }

        private Dictionary<string, ControlInfo> ctrlDict = new Dictionary<string, ControlInfo>();

        public void FindAllControls(Control thisCtrl)
        {
            foreach (Control ctl in thisCtrl.Controls)
            {
                try
                {
                    if (!(ctl.Parent == null))
                    {
                        int parentHeight = ctl.Parent.Height;
                        int parentWidth = ctl.Parent.Width;

                        ControlInfo c = new ControlInfo();
                        c.name = ctl.Name;
                        c.parentName = ctl.Parent.Name;
                        c.topOffsetPercent = Convert.ToDouble(ctl.Top) / Convert.ToDouble(parentHeight);
                        c.leftOffsetPercent = Convert.ToDouble(ctl.Left) / Convert.ToDouble(parentWidth);
                        c.heightPercent = Convert.ToDouble(ctl.Height) / Convert.ToDouble(parentHeight);
                        c.widthPercent = Convert.ToDouble(ctl.Width) / Convert.ToDouble(parentWidth);
                        c.originalFontSize = ctl.Font.Size;
                        c.originalHeight = ctl.Height;
                        c.originalWidth = ctl.Width;
                        ctrlDict.Add(c.name, c);
                    }
                }
                catch (Exception)
                {
                }

                if (ctl.Controls.Count > 0)
                {
                    FindAllControls(ctl);
                }

            }
        }
        public void ResizeAllControls(Control thisCtrl)
        {
            Single fontRatioW;
            Single fontRatioH;
            Single fontRatio;
            Font f;

            foreach (Control ctl in thisCtrl.Controls)
            {
                try
                {
                    if (ctl is ComboBox)
                    {
                        continue;
                    }
                    if (!(ctl.Parent == null))
                    {
                        int parentHeight = ctl.Parent.Height;
                        int parentWidth = ctl.Parent.Width;

                        ControlInfo c = new ControlInfo();

                        Boolean ret = false;
                        try
                        {
                            ret = ctrlDict.TryGetValue(ctl.Name, out c);

                            // If found, adjust the current control based on control relative
                            // size and position information stored in the dictionary
                            if (ret)
                            {
                                ctl.Width = (int) (parentWidth * c.widthPercent);
                                ctl.Height = (int) (parentHeight * c.heightPercent);

                                // Position
                                ctl.Top = (int) (parentHeight * c.topOffsetPercent);
                                ctl.Left = (int) (parentWidth * c.leftOffsetPercent);

                                // Font
                                f = ctl.Font;
                                fontRatioW = ctl.Width / c.originalWidth;
                                fontRatioH = ctl.Height / c.originalHeight;
                                fontRatio = (fontRatioW + fontRatioH) / 2; // average change in control Height and Width
                                ctl.Font = new Font(f.FontFamily, c.originalFontSize * fontRatio, f.Style);

                            }

                        }
                        catch (Exception)
                        {
                        }

                        if (ctl.Controls.Count > 0)
                        {
                            ResizeAllControls(ctl);
                        }

                    }
                }
                catch (Exception)
                {

                }
            }
        }


    }
}
