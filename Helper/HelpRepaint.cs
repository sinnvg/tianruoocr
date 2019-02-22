﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TrOCR
{

	public class HelpRepaint
	{

		public class ColorItemx
		{

			public ColorItemx(string name, Color clr)
			{
				Name = name;
				ItemColor = clr;
			}

			// (get) Token: 0x060001C9 RID: 457 RVA: 0x0001B564 File Offset: 0x00019764
			// (set) Token: 0x060001CA RID: 458 RVA: 0x00002D3D File Offset: 0x00000F3D
			public Color ItemColor
			{
				get
				{
					return color;
				}
				set
				{
					color = value;
				}
			}

			// (get) Token: 0x060001CB RID: 459 RVA: 0x0001B57C File Offset: 0x0001977C
			// (set) Token: 0x060001CC RID: 460 RVA: 0x00002D47 File Offset: 0x00000F47
			public string Name
			{
				get
				{
					return name;
				}
				set
				{
					name = value;
				}
			}

			public string name;

			public Color color;
		}

		public class HWColorPicker : FloatLayerBase
		{

			// (get) Token: 0x060001CD RID: 461 RVA: 0x0001B594 File Offset: 0x00019794
			public Color SelectedColor
			{
				get
				{
					return selectedColor;
				}
			}

			// (get) Token: 0x060001CE RID: 462 RVA: 0x0001B5AC File Offset: 0x000197AC
			// (set) Token: 0x060001CF RID: 463 RVA: 0x00002D51 File Offset: 0x00000F51
			[DefaultValue(true)]
			[Description("是否显示颜色提示")]
			public bool ShowTip
			{
				get
				{
					return showTip;
				}
				set
				{
					showTip = value;
				}
			}

			// (get) Token: 0x060001D0 RID: 464 RVA: 0x0001B5C4 File Offset: 0x000197C4
			// (set) Token: 0x060001D1 RID: 465 RVA: 0x0001B5DC File Offset: 0x000197DC
			[DefaultValue(typeof(Color), "255, 238, 194")]
			[Description("高亮背景色")]
			public Color HoverBKColor
			{
				get
				{
					return hoverBKColor;
				}
				set
				{
					var flag = hoverBKColor != value;
					var flag2 = flag;
					var flag3 = flag2;
					var flag4 = flag3;
					var flag5 = flag4;
					var flag6 = flag5;
					var flag7 = flag6;
					var flag8 = flag7;
					var flag9 = flag8;
					var flag10 = flag9;
					if (flag10)
					{
						var flag11 = hoverBrush != null;
						var flag12 = flag11;
						var flag13 = flag12;
						var flag14 = flag13;
						var flag15 = flag14;
						var flag16 = flag15;
						var flag17 = flag16;
						var flag18 = flag17;
						var flag19 = flag18;
						var flag20 = flag19;
						if (flag20)
						{
							hoverBrush.Dispose();
						}
						hoverBrush = new SolidBrush(value);
					}
					hoverBKColor = value;
				}
			}

			// (get) Token: 0x060001D2 RID: 466 RVA: 0x0001B670 File Offset: 0x00019870
			public List<ColorItemx> ColorTable
			{
				get
				{
					return colorTable;
				}
			}

			public HWColorPicker()
			{
				Font = new Font(Font.Name, 9f / StaticValue.Dpifactor, Font.Style, Font.Unit, Font.GdiCharSet, Font.GdiVerticalFont);
				hoverItem = -1;
				InitializeComponent();
				InitColor();
				CalcWindowSize();
				sf = new StringFormat();
				sf.Alignment = StringAlignment.Center;
				sf.LineAlignment = StringAlignment.Center;
				HoverBKColor = Color.FromArgb(255, 238, 194);
				ShowTip = true;
			}

			public void CalcWindowSize()
			{
				var width = 152;
				var height = 100;
				Size = new Size(width, height);
			}

			public Rectangle CalcItemBound(int index)
			{
				var flag = index < 0 || index > 40;
				var flag2 = flag;
				var flag3 = flag2;
				var flag4 = flag3;
				var flag5 = flag4;
				var flag6 = flag5;
				var flag7 = flag6;
				var flag8 = flag7;
				var flag9 = flag8;
				var flag10 = flag9;
				if (flag10)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				var flag11 = index == 40;
				var flag12 = flag11;
				var flag13 = flag12;
				var flag14 = flag13;
				var flag15 = flag14;
				var flag16 = flag15;
				var flag17 = flag16;
				var flag18 = flag17;
				var flag19 = flag18;
				var flag20 = flag19;
				Rectangle result;
				if (flag20)
				{
					result = Rectangle.FromLTRB(0, 0, 0, 0);
				}
				else
				{
					result = new Rectangle(index % 8 * 18 + 3, index / 8 * 18 + 3, 18, 18);
				}
				return result;
			}

			public int GetIndexFromPoint(Point point)
			{
				int result;
				if (point.X <= 3 || point.X >= Width - 3 || point.Y <= 3 || point.Y >= Height - 3)
				{
					result = -1;
				}
				else
				{
					var num = (point.Y - 3) / 18;
					if (num > 4)
					{
						result = 40;
					}
					else
					{
						var num2 = (point.X - 3) / 18;
						result = num * 8 + num2;
					}
				}
				return result;
			}

			public void SelectColor(Color clr)
			{
				selectedColor = clr;
				var flag = SelectedColorChanged != null;
				var flag2 = flag;
				var flag3 = flag2;
				var flag4 = flag3;
				var flag5 = flag4;
				var flag6 = flag5;
				var flag7 = flag6;
				var flag8 = flag7;
				var flag9 = flag8;
				var flag10 = flag9;
				if (flag10)
				{
					SelectedColorChanged(ctl_T, EventArgs.Empty);
				}
				Hide();
			}

			public void DrawItem(DrawItemEventArgs e)
			{
				var bounds = e.Bounds;
				bounds.Inflate(-1, -1);
				e.DrawBackground();
				var flag = (e.State & DrawItemState.HotLight) > DrawItemState.None;
				var flag2 = flag;
				var flag3 = flag2;
				var flag4 = flag3;
				var flag5 = flag4;
				var flag6 = flag5;
				var flag7 = flag6;
				var flag8 = flag7;
				var flag9 = flag8;
				var flag10 = flag9;
				if (flag10)
				{
					e.Graphics.FillRectangle(hoverBrush, bounds);
					e.Graphics.DrawRectangle(Pens.Black, bounds);
				}
				var flag11 = e.Index == 40;
				var flag12 = !flag11;
				var flag13 = flag12;
				var flag14 = flag13;
				var flag15 = flag14;
				var flag16 = flag15;
				var flag17 = flag16;
				var flag18 = flag17;
				var flag19 = flag18;
				var flag20 = flag19;
				if (flag20)
				{
					var bounds2 = e.Bounds;
					bounds2.Inflate(-3, -3);
					using (Brush brush = new SolidBrush(ColorTable[e.Index].ItemColor))
					{
						e.Graphics.FillRectangle(brush, bounds2);
					}
					e.Graphics.DrawRectangle(Pens.Gray, bounds2);
				}
			}

			public void HWColorPicker_Paint(object sender, PaintEventArgs e)
			{
				e.Graphics.DrawRectangle(Pens.LightGray, 0, 0, 150, 98);
				for (var i = 0; i < 40; i++)
				{
					var e2 = new DrawItemEventArgs(e.Graphics, Font, CalcItemBound(i), i, DrawItemState.Default, ForeColor, BackColor);
					DrawItem(e2);
				}
			}

			public void HWColorPicker_MouseMove(object sender, MouseEventArgs e)
			{
				var indexFromPoint = GetIndexFromPoint(e.Location);
				if (indexFromPoint != hoverItem)
				{
					var graphics = CreateGraphics();
					if (hoverItem != -1)
					{
						DrawItem(new DrawItemEventArgs(graphics, Font, CalcItemBound(hoverItem), hoverItem, DrawItemState.Default));
					}
					if (indexFromPoint <= 40)
					{
						if (indexFromPoint != -1)
						{
							if (ShowTip)
							{
								ShowToolTip(indexFromPoint);
							}
							DrawItem(new DrawItemEventArgs(graphics, Font, CalcItemBound(indexFromPoint), indexFromPoint, DrawItemState.Default | DrawItemState.HotLight));
						}
						graphics.Dispose();
						hoverItem = indexFromPoint;
					}
				}
			}

			public void HWColorPicker_MouseClick(object sender, MouseEventArgs e)
			{
				var indexFromPoint = GetIndexFromPoint(e.Location);
				if (indexFromPoint != -1 && indexFromPoint != 40)
				{
					SelectedColored = colorTable[indexFromPoint].ItemColor;
					DialogResult = DialogResult.OK;
				}
			}

			public void ShowToolTip(int index)
			{
				var flag = index == 40;
				var flag2 = !flag;
				var flag3 = flag2;
				var flag4 = flag3;
				var flag5 = flag4;
				var flag6 = flag5;
				var flag7 = flag6;
				var flag8 = flag7;
				var flag9 = flag8;
				var flag10 = flag9;
				if (flag10)
				{
					tip.SetToolTip(this, ColorTable[index].Name);
				}
			}

			public void InitColor()
			{
				colorTable = new List<ColorItemx>();
				colorTable.AddRange(new[]
				{
					new ColorItemx("黑色", Color.FromArgb(0, 0, 0)),
					new ColorItemx("褐色", Color.FromArgb(153, 51, 0)),
					new ColorItemx("橄榄色", Color.FromArgb(51, 51, 0)),
					new ColorItemx("深绿", Color.FromArgb(0, 51, 0)),
					new ColorItemx("深青", Color.FromArgb(0, 51, 102)),
					new ColorItemx("深蓝", Color.FromArgb(0, 0, 128)),
					new ColorItemx("靛蓝", Color.FromArgb(51, 51, 153)),
					new ColorItemx("灰色-80%", Color.FromArgb(51, 51, 51)),
					new ColorItemx("深红", Color.FromArgb(128, 0, 0)),
					new ColorItemx("橙色", Color.FromArgb(255, 102, 0)),
					new ColorItemx("深黄", Color.FromArgb(128, 128, 0)),
					new ColorItemx("绿色", Color.FromArgb(0, 128, 0)),
					new ColorItemx("青色", Color.FromArgb(0, 128, 128)),
					new ColorItemx("蓝色", Color.FromArgb(0, 0, 255)),
					new ColorItemx("蓝灰", Color.FromArgb(102, 102, 153)),
					new ColorItemx("灰色-50%", Color.FromArgb(128, 128, 128)),
					new ColorItemx("红色", Color.FromArgb(255, 0, 0)),
					new ColorItemx("浅橙", Color.FromArgb(255, 153, 0)),
					new ColorItemx("酸橙", Color.FromArgb(153, 204, 0)),
					new ColorItemx("海绿", Color.FromArgb(51, 153, 102)),
					new ColorItemx("水绿", Color.FromArgb(51, 204, 204)),
					new ColorItemx("浅蓝", Color.FromArgb(51, 102, 255)),
					new ColorItemx("紫罗兰", Color.FromArgb(128, 0, 128)),
					new ColorItemx("灰色-40%", Color.FromArgb(153, 153, 153)),
					new ColorItemx("粉红", Color.FromArgb(255, 0, 255)),
					new ColorItemx("金色", Color.FromArgb(255, 204, 0)),
					new ColorItemx("黄色", Color.FromArgb(255, 255, 0)),
					new ColorItemx("鲜绿", Color.FromArgb(0, 255, 0)),
					new ColorItemx("青绿", Color.FromArgb(0, 255, 255)),
					new ColorItemx("天蓝", Color.FromArgb(0, 204, 255)),
					new ColorItemx("梅红", Color.FromArgb(153, 51, 102)),
					new ColorItemx("灰色-25%", Color.FromArgb(192, 192, 192)),
					new ColorItemx("玫瑰红", Color.FromArgb(255, 153, 204)),
					new ColorItemx("茶色", Color.FromArgb(255, 204, 153)),
					new ColorItemx("浅黄", Color.FromArgb(255, 255, 204)),
					new ColorItemx("浅绿", Color.FromArgb(204, 255, 204)),
					new ColorItemx("浅青绿", Color.FromArgb(204, 255, 255)),
					new ColorItemx("淡蓝", Color.FromArgb(153, 204, 255)),
					new ColorItemx("淡紫", Color.FromArgb(204, 153, 255)),
					new ColorItemx("白色", Color.FromArgb(255, 255, 255))
				});
			}

			protected override void Dispose(bool disposing)
			{
				var flag = disposing && components != null;
				var flag2 = flag;
				var flag3 = flag2;
				var flag4 = flag3;
				var flag5 = flag4;
				var flag6 = flag5;
				var flag7 = flag6;
				var flag8 = flag7;
				var flag9 = flag8;
				var flag10 = flag9;
				if (flag10)
				{
					components.Dispose();
				}
				base.Dispose(disposing);
			}

			public void InitializeComponent()
			{
				components = new Container();
				tip = new ToolTip(components);
				SuspendLayout();
				BackColor = Color.White;
				AutoScaleMode = AutoScaleMode.None;
				Name = "HWColorPicker";
				Paint += HWColorPicker_Paint;
				MouseClick += HWColorPicker_MouseClick;
				MouseMove += HWColorPicker_MouseMove;
				ClientSize = new Size(114, 115);
				SizeGripStyle = SizeGripStyle.Hide;
				ResumeLayout(false);
			}

			// (get) Token: 0x060001E0 RID: 480 RVA: 0x00002D5B File Offset: 0x00000F5B
			// (set) Token: 0x060001E1 RID: 481 RVA: 0x00002D63 File Offset: 0x00000F63
			public Color SelectedColored { get; private set; }

			[CompilerGenerated]
			private EventHandler SelectedColorChanged;

			public bool showTip;

			public Color selectedColor;

			public Color hoverBKColor;

			public List<ColorItemx> colorTable;

			public StringFormat sf;

			public int hoverItem;

			public Control ctl;

			public Brush hoverBrush;

			public IContainer components;

			public ToolTip tip;

			public ToolStripButton ctl_T;
		}

		public class MenuItemRenderer : ToolStripProfessionalRenderer
		{

			public MenuItemRenderer()
			{
				textFont = new Font("微软雅黑", 9f / StaticValue.Dpifactor, FontStyle.Regular, GraphicsUnit.Point, 0);
				menuItemSelectedColor = Color.White;
				menuItemBorderColor = Color.White;
				menuItemSelectedColor = Color.White;
				menuItemBorderColor = Color.LightSlateGray;
			}

			protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
			{
				var isOnDropDown = e.Item.IsOnDropDown;
				var flag = isOnDropDown;
				var flag2 = flag;
				var flag3 = flag2;
				var flag4 = flag3;
				var flag5 = flag4;
				var flag6 = flag5;
				var flag7 = flag6;
				var flag8 = flag7;
				var flag9 = flag8;
				if (flag9)
				{
					var flag10 = e.Item.Selected && e.Item.Enabled;
					var flag11 = flag10;
					var flag12 = flag11;
					var flag13 = flag12;
					var flag14 = flag13;
					var flag15 = flag14;
					var flag16 = flag15;
					var flag17 = flag16;
					var flag18 = flag17;
					var flag19 = flag18;
					if (flag19)
					{
						DrawMenuDropDownItemHighlight(e);
					}
				}
				else
				{
					base.OnRenderMenuItemBackground(e);
				}
			}

			public void DrawMenuDropDownItemHighlight(ToolStripItemRenderEventArgs e)
			{
				var rect = default(Rectangle);
				rect = new Rectangle(2, 0, (int)e.Graphics.VisibleClipBounds.Width - 36, (int)e.Graphics.VisibleClipBounds.Height - 1);
				using (var pen = new Pen(menuItemBorderColor))
				{
					e.Graphics.DrawRectangle(pen, rect);
				}
			}

			protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
			{
				var toolStrip = e.ToolStrip;
				e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
				var flag = e.ToolStrip is MenuStrip || e.ToolStrip is ToolStripDropDown;
				var flag2 = flag;
				var flag3 = flag2;
				var flag4 = flag3;
				var flag5 = flag4;
				var flag6 = flag5;
				var flag7 = flag6;
				var flag8 = flag7;
				var flag9 = flag8;
				var flag10 = flag9;
				if (flag10)
				{
					e.Graphics.DrawRectangle(new Pen(Color.LightSlateGray), new Rectangle(0, 0, e.AffectedBounds.Width - 1, e.AffectedBounds.Height - 1));
				}
				else
				{
					var flag11 = toolStrip != null;
					var flag12 = flag11;
					var flag13 = flag12;
					var flag14 = flag13;
					var flag15 = flag14;
					var flag16 = flag15;
					var flag17 = flag16;
					var flag18 = flag17;
					var flag19 = flag18;
					var flag20 = flag19;
					if (flag20)
					{
						e.Graphics.DrawRectangle(new Pen(Color.White), new Rectangle(0, 0, e.AffectedBounds.Width - 1, e.AffectedBounds.Height - 1));
					}
					base.OnRenderToolStripBorder(e);
				}
			}

			protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
			{
				var graphics = e.Graphics;
				graphics.SmoothingMode = SmoothingMode.HighQuality;
				var item = e.Item;
				var selected = item.Selected;
				var flag = selected;
				var flag2 = flag;
				var flag3 = flag2;
				var flag4 = flag3;
				var flag5 = flag4;
				var flag6 = flag5;
				var flag7 = flag6;
				var flag8 = flag7;
				var flag9 = flag8;
				if (flag9)
				{
					var pen = new Pen(Color.LightSlateGray);
					var points = new[]
					{
						new Point(0, 0),
						new Point(item.Size.Width - 1, 0),
						new Point(item.Size.Width - 1, item.Size.Height - 3),
						new Point(0, item.Size.Height - 3),
						new Point(0, 0)
					};
					graphics.DrawLines(pen, points);
				}
				else
				{
					base.OnRenderMenuItemBackground(e);
				}
			}

			protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
			{
				var graphics = e.Graphics;
				var flag = e.ToolStrip is ToolStripDropDown;
				var flag2 = flag;
				var flag3 = flag2;
				var flag4 = flag3;
				var flag5 = flag4;
				var flag6 = flag5;
				var flag7 = flag6;
				var flag8 = flag7;
				var flag9 = flag8;
				var flag10 = flag9;
				if (flag10)
				{
					var brush = new LinearGradientBrush(new Point(0, 0), new Point(e.Item.Width, 0), Color.LightGray, Color.FromArgb(0, Color.LightGray));
					graphics.FillRectangle(brush, new Rectangle(33, e.Item.Height / 2, e.Item.Width / 4 * 3, 1));
				}
			}

			protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
			{
				var item = e.Item;
				var graphics = e.Graphics;
				graphics.SmoothingMode = SmoothingMode.HighQuality;
				var item2 = e.Item;
				var selected = item.Selected;
				var flag = selected;
				var flag2 = flag;
				var flag3 = flag2;
				var flag4 = flag3;
				var flag5 = flag4;
				var flag6 = flag5;
				var flag7 = flag6;
				var flag8 = flag7;
				var flag9 = flag8;
				if (flag9)
				{
					var pen = new Pen(Color.LightSlateGray);
					var points = new[]
					{
						new Point(0, 0),
						new Point(item.Size.Width - 1, 0),
						new Point(item.Size.Width - 1, item.Size.Height - 3),
						new Point(0, item.Size.Height - 3),
						new Point(0, 0)
					};
					graphics.DrawLines(pen, points);
				}
				else
				{
					base.OnRenderMenuItemBackground(e);
				}
			}

			public Font textFont;

			public Color menuItemSelectedColor;

			public Color menuItemBorderColor;
		}

		public class MenuItemRendererT : ToolStripProfessionalRenderer
		{

			public MenuItemRendererT()
			{
				textFont = new Font("微软雅黑", 9f / StaticValue.Dpifactor, FontStyle.Regular, GraphicsUnit.Point, 0);
				menuItemSelectedColor = Color.White;
				menuItemBorderColor = Color.White;
				menuItemSelectedColor = Color.White;
				menuItemBorderColor = Color.LightSlateGray;
			}

			protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
			{
				var isOnDropDown = e.Item.IsOnDropDown;
				var flag = isOnDropDown;
				var flag2 = flag;
				var flag3 = flag2;
				var flag4 = flag3;
				var flag5 = flag4;
				var flag6 = flag5;
				var flag7 = flag6;
				var flag8 = flag7;
				var flag9 = flag8;
				if (flag9)
				{
					var flag10 = e.Item.Selected && e.Item.Enabled;
					var flag11 = flag10;
					var flag12 = flag11;
					var flag13 = flag12;
					var flag14 = flag13;
					var flag15 = flag14;
					var flag16 = flag15;
					var flag17 = flag16;
					var flag18 = flag17;
					var flag19 = flag18;
					if (flag19)
					{
						DrawMenuDropDownItemHighlight(e);
					}
				}
				else
				{
					base.OnRenderMenuItemBackground(e);
				}
			}

			public void DrawMenuDropDownItemHighlight(ToolStripItemRenderEventArgs e)
			{
				var rect = default(Rectangle);
				rect = new Rectangle(2, 0, (int)e.Graphics.VisibleClipBounds.Width - 4, (int)e.Graphics.VisibleClipBounds.Height - 1);
				using (var pen = new Pen(menuItemBorderColor))
				{
					e.Graphics.DrawRectangle(pen, rect);
				}
			}

			public Font textFont;

			public Color menuItemSelectedColor;

			public Color menuItemBorderColor;
		}

		public class myWebBroswer : WebBrowser
		{

			public override bool PreProcessMessage(ref Message msg)
			{
				var flag = msg.Msg == 256 && (int)msg.WParam == 86 && ModifierKeys == Keys.Control;
				var flag2 = flag;
				var flag3 = flag2;
				var flag4 = flag3;
				var flag5 = flag4;
				var flag6 = flag5;
				var flag7 = flag6;
				var flag8 = flag7;
				var flag9 = flag8;
				var flag10 = flag9;
				if (flag10)
				{
					Clipboard.SetDataObject((string)Clipboard.GetDataObject().GetData(DataFormats.UnicodeText));
					HelpWin32.keybd_event(Keys.ControlKey, 0, 0u, 0u);
					HelpWin32.keybd_event(Keys.D9, 0, 0u, 0u);
					HelpWin32.keybd_event(Keys.D9, 0, 2u, 0u);
					HelpWin32.keybd_event(Keys.ControlKey, 0, 2u, 0u);
				}
				var flag11 = msg.Msg == 256 && (int)msg.WParam == 67 && ModifierKeys == Keys.Control;
				var flag12 = flag11;
				var flag13 = flag12;
				var flag14 = flag13;
				var flag15 = flag14;
				var flag16 = flag15;
				var flag17 = flag16;
				var flag18 = flag17;
				var flag19 = flag18;
				var flag20 = flag19;
				if (flag20)
				{
					HelpWin32.keybd_event(Keys.ControlKey, 0, 0u, 0u);
					HelpWin32.keybd_event(Keys.D8, 0, 0u, 0u);
					HelpWin32.keybd_event(Keys.D8, 0, 2u, 0u);
					HelpWin32.keybd_event(Keys.ControlKey, 0, 2u, 0u);
				}
				return base.PreProcessMessage(ref msg);
			}
		}

		[Description("ToolStripItem that allows selecting a color from a color picker control.")]
		[DefaultEvent("SelectedColorChanged")]
		[ToolboxBitmap(typeof(ToolStripColorPicker), "ToolStripColorPicker")]
		[DefaultProperty("Color")]
		[ToolboxItem(false)]
		public class ToolStripColorPicker : ToolStripButton
		{

			public Point GetOpenPoint()
			{
				var flag = Owner == null;
				var flag2 = flag;
				var flag3 = flag2;
				var flag4 = flag3;
				var flag5 = flag4;
				var flag6 = flag5;
				var flag7 = flag6;
				var flag8 = flag7;
				var flag9 = flag8;
				var flag10 = flag9;
				Point result;
				if (flag10)
				{
					result = new Point(5, 5);
				}
				else
				{
					var num = 0;
					foreach (var obj in Parent.Items)
					{
						var toolStripItem = (ToolStripItem)obj;
						var flag11 = toolStripItem == this;
						var flag12 = flag11;
						var flag13 = flag12;
						var flag14 = flag13;
						var flag15 = flag14;
						var flag16 = flag15;
						var flag17 = flag16;
						var flag18 = flag17;
						var flag19 = flag18;
						var flag20 = flag19;
						if (flag20)
						{
							break;
						}
						num += toolStripItem.Width;
					}
					result = new Point(num, -4);
				}
				return result;
			}

			// (get) Token: 0x060001EF RID: 495 RVA: 0x0001CAB8 File Offset: 0x0001ACB8
			// (set) Token: 0x060001F0 RID: 496 RVA: 0x0000237B File Offset: 0x0000057B
			public Point GetPoint
			{
				get
				{
					return GetOpenPoint();
				}
				set
				{
				}
			}
		}

		public class ToolStripEx : ToolStrip
		{

			protected override void WndProc(ref Message m)
			{
				if (m.Msg == 33 && CanFocus && !Focused)
				{
					Focus();
				}
				base.WndProc(ref m);
			}
		}

		public class FloatLayerBase : Form
		{

			// (get) Token: 0x060001F4 RID: 500 RVA: 0x0001CAD0 File Offset: 0x0001ACD0
			// (set) Token: 0x060001F5 RID: 501 RVA: 0x0001CAE8 File Offset: 0x0001ACE8
			[DefaultValue(BorderStyle.Fixed3D)]
			[Description("获取或设置边框类型。")]
			public BorderStyle BorderType
			{
				get
				{
					return _borderType;
				}
				set
				{
					var flag = _borderType != value;
					var flag2 = flag;
					var flag3 = flag2;
					var flag4 = flag3;
					var flag5 = flag4;
					var flag6 = flag5;
					var flag7 = flag6;
					var flag8 = flag7;
					var flag9 = flag8;
					var flag10 = flag9;
					if (flag10)
					{
						_borderType = value;
						Invalidate();
					}
				}
			}

			// (get) Token: 0x060001F6 RID: 502 RVA: 0x0001CB34 File Offset: 0x0001AD34
			// (set) Token: 0x060001F7 RID: 503 RVA: 0x0001CB4C File Offset: 0x0001AD4C
			[DefaultValue(Border3DStyle.RaisedInner)]
			[Description("获取或设置三维边框样式。")]
			public Border3DStyle Border3DStyle
			{
				get
				{
					return _border3DStyle;
				}
				set
				{
					var flag = _border3DStyle != value;
					var flag2 = flag;
					var flag3 = flag2;
					var flag4 = flag3;
					var flag5 = flag4;
					var flag6 = flag5;
					var flag7 = flag6;
					var flag8 = flag7;
					var flag9 = flag8;
					var flag10 = flag9;
					if (flag10)
					{
						_border3DStyle = value;
						Invalidate();
					}
				}
			}

			// (get) Token: 0x060001F8 RID: 504 RVA: 0x0001CB98 File Offset: 0x0001AD98
			// (set) Token: 0x060001F9 RID: 505 RVA: 0x0001CBB0 File Offset: 0x0001ADB0
			[DefaultValue(ButtonBorderStyle.Solid)]
			[Description("获取或设置线型边框样式。")]
			public ButtonBorderStyle BorderSingleStyle
			{
				get
				{
					return _borderSingleStyle;
				}
				set
				{
					var flag = _borderSingleStyle != value;
					var flag2 = flag;
					var flag3 = flag2;
					var flag4 = flag3;
					var flag5 = flag4;
					var flag6 = flag5;
					var flag7 = flag6;
					var flag8 = flag7;
					var flag9 = flag8;
					var flag10 = flag9;
					if (flag10)
					{
						_borderSingleStyle = value;
						Invalidate();
					}
				}
			}

			// (get) Token: 0x060001FA RID: 506 RVA: 0x0001CBFC File Offset: 0x0001ADFC
			// (set) Token: 0x060001FB RID: 507 RVA: 0x0001CC14 File Offset: 0x0001AE14
			[DefaultValue(typeof(Color), "DarkGray")]
			[Description("获取或设置边框颜色（仅当边框类型为线型时有效）。")]
			public Color BorderColor
			{
				get
				{
					return _borderColor;
				}
				set
				{
					var flag = !(_borderColor == value);
					var flag2 = flag;
					var flag3 = flag2;
					var flag4 = flag3;
					var flag5 = flag4;
					var flag6 = flag5;
					var flag7 = flag6;
					var flag8 = flag7;
					var flag9 = flag8;
					var flag10 = flag9;
					if (flag10)
					{
						_borderColor = value;
						Invalidate();
					}
				}
			}

			// (get) Token: 0x060001FC RID: 508 RVA: 0x0001CC64 File Offset: 0x0001AE64
			protected sealed override CreateParams CreateParams
			{
				get
				{
					var createParams = base.CreateParams;
					createParams.Style |= 1073741824;
					createParams.Style |= 67108864;
					createParams.Style |= 65536;
					createParams.Style &= -262145;
					createParams.Style &= -8388609;
					createParams.Style &= -4194305;
					createParams.ExStyle = 0;
					createParams.ExStyle |= 65536;
					return createParams;
				}
			}

			public FloatLayerBase()
			{
				_mouseMsgFilter = new AppMouseMessageHandler(this);
				InitBaseProperties();
				_borderType = BorderStyle.Fixed3D;
				_border3DStyle = Border3DStyle.RaisedInner;
				_borderSingleStyle = ButtonBorderStyle.Solid;
				_borderColor = Color.DarkGray;
			}

			protected override void OnLoad(EventArgs e)
			{
				var flag = !_isShowDialogAgain;
				var flag2 = flag;
				var flag3 = flag2;
				var flag4 = flag3;
				var flag5 = flag4;
				var flag6 = flag5;
				var flag7 = flag6;
				var flag8 = flag7;
				var flag9 = flag8;
				var flag10 = flag9;
				if (flag10)
				{
					var flag11 = !DesignMode;
					var flag12 = flag11;
					var flag13 = flag12;
					var flag14 = flag13;
					var flag15 = flag14;
					var flag16 = flag15;
					var flag17 = flag16;
					var flag18 = flag17;
					var flag19 = flag18;
					var flag20 = flag19;
					if (flag20)
					{
						var frameBorderSize = SystemInformation.FrameBorderSize;
						Size -= frameBorderSize + frameBorderSize;
					}
					base.OnLoad(e);
				}
			}

			protected override void OnShown(EventArgs e)
			{
				var flag = !_isShowDialogAgain;
				var flag2 = flag;
				var flag3 = flag2;
				var flag4 = flag3;
				var flag5 = flag4;
				var flag6 = flag5;
				var flag7 = flag6;
				var flag8 = flag7;
				var flag9 = flag8;
				var flag10 = flag9;
				if (flag10)
				{
					var modal = Modal;
					var flag11 = modal;
					var flag12 = flag11;
					var flag13 = flag12;
					var flag14 = flag13;
					var flag15 = flag14;
					var flag16 = flag15;
					var flag17 = flag16;
					var flag18 = flag17;
					var flag19 = flag18;
					if (flag19)
					{
						_isShowDialogAgain = true;
					}
					Control control = null;
					var flag20 = !DesignMode && (control = GetNextControl(this, true)) != null;
					var flag21 = flag20;
					var flag22 = flag21;
					var flag23 = flag22;
					var flag24 = flag23;
					var flag25 = flag24;
					var flag26 = flag25;
					var flag27 = flag26;
					var flag28 = flag27;
					var flag29 = flag28;
					if (flag29)
					{
						control.Focus();
					}
					base.OnShown(e);
				}
			}

			protected override void WndProc(ref Message m)
			{
				var flag = m.Msg == 24 && m.WParam != IntPtr.Zero && m.LParam == IntPtr.Zero && Modal && Owner != null && !Owner.IsDisposed;
				var flag2 = flag;
				var flag3 = flag2;
				var flag4 = flag3;
				var flag5 = flag4;
				var flag6 = flag5;
				var flag7 = flag6;
				var flag8 = flag7;
				var flag9 = flag8;
				var flag10 = flag9;
				if (flag10)
				{
					var isMdiChild = Owner.IsMdiChild;
					var flag11 = isMdiChild;
					var flag12 = flag11;
					var flag13 = flag12;
					var flag14 = flag13;
					var flag15 = flag14;
					var flag16 = flag15;
					var flag17 = flag16;
					var flag18 = flag17;
					var flag19 = flag18;
					if (flag19)
					{
						NativeMethods.EnableWindow(Owner.MdiParent.Handle, true);
						NativeMethods.SetParent(Handle, Owner.Handle);
					}
					else
					{
						NativeMethods.EnableWindow(Owner.Handle, true);
					}
				}
				base.WndProc(ref m);
			}

			protected override void OnPaintBackground(PaintEventArgs e)
			{
				base.OnPaintBackground(e);
				var flag = _borderType == BorderStyle.Fixed3D;
				var flag2 = flag;
				var flag3 = flag2;
				var flag4 = flag3;
				var flag5 = flag4;
				var flag6 = flag5;
				var flag7 = flag6;
				var flag8 = flag7;
				var flag9 = flag8;
				var flag10 = flag9;
				if (flag10)
				{
					ControlPaint.DrawBorder3D(e.Graphics, ClientRectangle, Border3DStyle);
				}
				else
				{
					var flag11 = _borderType == BorderStyle.FixedSingle;
					var flag12 = flag11;
					var flag13 = flag12;
					var flag14 = flag13;
					var flag15 = flag14;
					var flag16 = flag15;
					var flag17 = flag16;
					var flag18 = flag17;
					var flag19 = flag18;
					var flag20 = flag19;
					if (flag20)
					{
						ControlPaint.DrawBorder(e.Graphics, ClientRectangle, BorderColor, BorderSingleStyle);
					}
				}
			}

			protected override void OnVisibleChanged(EventArgs e)
			{
				var flag = !DesignMode;
				var flag2 = flag;
				var flag3 = flag2;
				var flag4 = flag3;
				var flag5 = flag4;
				var flag6 = flag5;
				var flag7 = flag6;
				var flag8 = flag7;
				var flag9 = flag8;
				var flag10 = flag9;
				if (flag10)
				{
					var visible = Visible;
					var flag11 = visible;
					var flag12 = flag11;
					var flag13 = flag12;
					var flag14 = flag13;
					var flag15 = flag14;
					var flag16 = flag15;
					var flag17 = flag16;
					var flag18 = flag17;
					var flag19 = flag18;
					if (flag19)
					{
						Application.AddMessageFilter(_mouseMsgFilter);
					}
					else
					{
						Application.RemoveMessageFilter(_mouseMsgFilter);
					}
				}
				base.OnVisibleChanged(e);
			}

			public DialogResult ShowDialog(Control control)
			{
				return ShowDialog(control, 0, control.Height);
			}

			public DialogResult ShowDialog(Control control, int offsetX, int offsetY)
			{
				return ShowDialog(control, new Point(offsetX, offsetY));
			}

			public DialogResult ShowDialog(Control control, Point offset)
			{
				return ShowDialogInternal(control, offset);
			}

			public DialogResult ShowDialog(ToolStripItem item)
			{
				return ShowDialog(item, 0, item.Height + 4);
			}

			public DialogResult ShowDialog(ToolStripItem item, int offsetX, int offsetY)
			{
				return ShowDialog(item, new Point(offsetX, offsetY));
			}

			public DialogResult ShowDialog(ToolStripItem item, Point offset)
			{
				return ShowDialogInternal(item, offset);
			}

			public void Show(Control control)
			{
				Show(control, 0, control.Height);
			}

			public void Show(Control control, int offsetX, int offsetY)
			{
				Show(control, new Point(offsetX, offsetY));
			}

			public void Show(Control control, Point offset)
			{
				ShowInternal(control, offset);
			}

			public void Show(ToolStripItem item)
			{
				Show(item, 0, item.Height);
			}

			public void Show(ToolStripItem item, int offsetX, int offsetY)
			{
				Show(item, new Point(offsetX, offsetY));
			}

			public void Show(ToolStripItem item, Point offset)
			{
				ShowInternal(item, offset);
			}

			public DialogResult ShowDialogInternal(Component controlOrItem, Point offset)
			{
				var visible = Visible;
				var flag = visible;
				var flag2 = flag;
				var flag3 = flag2;
				var flag4 = flag3;
				var flag5 = flag4;
				var flag6 = flag5;
				var flag7 = flag6;
				var flag8 = flag7;
				var flag9 = flag8;
				DialogResult result;
				if (flag9)
				{
					result = DialogResult.None;
				}
				else
				{
					SetLocationAndOwner(controlOrItem, offset);
					result = base.ShowDialog();
				}
				return result;
			}

			public void ShowInternal(Component controlOrItem, Point offset)
			{
				var flag = !Visible;
				var flag2 = flag;
				var flag3 = flag2;
				var flag4 = flag3;
				var flag5 = flag4;
				var flag6 = flag5;
				var flag7 = flag6;
				var flag8 = flag7;
				var flag9 = flag8;
				var flag10 = flag9;
				if (flag10)
				{
					SetLocationAndOwner(controlOrItem, offset);
					base.Show();
				}
			}

			public void SetLocationAndOwner(Component controlOrItem, Point offset)
			{
				var empty = Point.Empty;
				var flag = controlOrItem is ToolStripItem;
				var flag2 = flag;
				var flag3 = flag2;
				var flag4 = flag3;
				var flag5 = flag4;
				var flag6 = flag5;
				var flag7 = flag6;
				var flag8 = flag7;
				var flag9 = flag8;
				var flag10 = flag9;
				if (flag10)
				{
					var toolStripItem = (ToolStripItem)controlOrItem;
					empty.Offset(toolStripItem.Bounds.Location);
					controlOrItem = toolStripItem.Owner;
				}
				var control = (Control)controlOrItem;
				empty.Offset(GetControlLocationInForm(control));
				empty.Offset(offset);
				Location = empty;
				Owner = control.FindForm();
			}

			public static Point GetControlLocationInForm(Control c)
			{
				var location = c.Location;
				while (!((c = c.Parent) is Form))
				{
					location.Offset(c.Location);
				}
				return location;
			}

			public void InitBaseProperties()
			{
				base.ControlBox = false;
				base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
				base.Text = string.Empty;
				base.HelpButton = false;
				base.Icon = null;
				base.IsMdiContainer = false;
				base.MaximizeBox = false;
				base.MinimizeBox = false;
				base.ShowIcon = false;
				base.ShowInTaskbar = false;
				base.StartPosition = FormStartPosition.Manual;
				base.TopMost = false;
				base.WindowState = FormWindowState.Normal;
			}

			[Obsolete("请使用别的重载！", true)]
			[EditorBrowsable(EditorBrowsableState.Never)]
			[Browsable(false)]
			public new DialogResult ShowDialog()
			{
				throw new NotImplementedException();
			}

			[Obsolete("请使用别的重载！", true)]
			[EditorBrowsable(EditorBrowsableState.Never)]
			[Browsable(false)]
			public new DialogResult ShowDialog(IWin32Window owner)
			{
				throw new NotImplementedException();
			}

			[Obsolete("请使用别的重载！", true)]
			[EditorBrowsable(EditorBrowsableState.Never)]
			[Browsable(false)]
			public new void Show()
			{
				throw new NotImplementedException();
			}

			[EditorBrowsable(EditorBrowsableState.Never)]
			[Browsable(false)]
			[Obsolete("请使用别的重载！", true)]
			public new void Show(IWin32Window owner)
			{
				throw new NotImplementedException();
			}

			// (get) Token: 0x06000218 RID: 536 RVA: 0x0001D36C File Offset: 0x0001B56C
			// (set) Token: 0x06000219 RID: 537 RVA: 0x0000237B File Offset: 0x0000057B
			[Obsolete("禁用该属性！", true)]
			[Browsable(false)]
			[EditorBrowsable(EditorBrowsableState.Never)]
			public new bool ControlBox
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			// (get) Token: 0x0600021A RID: 538 RVA: 0x0001D380 File Offset: 0x0001B580
			// (set) Token: 0x0600021B RID: 539 RVA: 0x0000237B File Offset: 0x0000057B
			[EditorBrowsable(EditorBrowsableState.Never)]
			[Browsable(false)]
			[Obsolete("设置边框请使用Border相关属性！", true)]
			public new FormBorderStyle FormBorderStyle
			{
				get
				{
					return FormBorderStyle.SizableToolWindow;
				}
				set
				{
				}
			}

			// (get) Token: 0x0600021C RID: 540 RVA: 0x0001D394 File Offset: 0x0001B594
			// (set) Token: 0x0600021D RID: 541 RVA: 0x0000237B File Offset: 0x0000057B
			[Browsable(false)]
			[Obsolete("禁用该属性！", true)]
			[EditorBrowsable(EditorBrowsableState.Never)]
			public sealed override string Text
			{
				get
				{
					return string.Empty;
				}
				set
				{
				}
			}

			// (get) Token: 0x0600021E RID: 542 RVA: 0x0001D36C File Offset: 0x0001B56C
			// (set) Token: 0x0600021F RID: 543 RVA: 0x0000237B File Offset: 0x0000057B
			[Obsolete("禁用该属性！", true)]
			[EditorBrowsable(EditorBrowsableState.Never)]
			[Browsable(false)]
			public new bool HelpButton
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			// (get) Token: 0x06000220 RID: 544 RVA: 0x0001D3AC File Offset: 0x0001B5AC
			// (set) Token: 0x06000221 RID: 545 RVA: 0x0000237B File Offset: 0x0000057B
			[EditorBrowsable(EditorBrowsableState.Never)]
			[Browsable(false)]
			[Obsolete("禁用该属性！", true)]
			public new Image Icon
			{
				get
				{
					return null;
				}
				set
				{
				}
			}

			// (get) Token: 0x06000222 RID: 546 RVA: 0x0001D36C File Offset: 0x0001B56C
			// (set) Token: 0x06000223 RID: 547 RVA: 0x0000237B File Offset: 0x0000057B
			[Obsolete("禁用该属性！", true)]
			[EditorBrowsable(EditorBrowsableState.Never)]
			[Browsable(false)]
			public new bool IsMdiContainer
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			// (get) Token: 0x06000224 RID: 548 RVA: 0x0001D36C File Offset: 0x0001B56C
			// (set) Token: 0x06000225 RID: 549 RVA: 0x0000237B File Offset: 0x0000057B
			[EditorBrowsable(EditorBrowsableState.Never)]
			[Obsolete("禁用该属性！", true)]
			[Browsable(false)]
			public new bool MaximizeBox
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			// (get) Token: 0x06000226 RID: 550 RVA: 0x0001D36C File Offset: 0x0001B56C
			// (set) Token: 0x06000227 RID: 551 RVA: 0x0000237B File Offset: 0x0000057B
			[EditorBrowsable(EditorBrowsableState.Never)]
			[Browsable(false)]
			[Obsolete("禁用该属性！", true)]
			public new bool MinimizeBox
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			// (get) Token: 0x06000228 RID: 552 RVA: 0x0001D36C File Offset: 0x0001B56C
			// (set) Token: 0x06000229 RID: 553 RVA: 0x0000237B File Offset: 0x0000057B
			[Browsable(false)]
			[EditorBrowsable(EditorBrowsableState.Never)]
			[Obsolete("禁用该属性！", true)]
			public new bool ShowIcon
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			// (get) Token: 0x0600022A RID: 554 RVA: 0x0001D36C File Offset: 0x0001B56C
			// (set) Token: 0x0600022B RID: 555 RVA: 0x0000237B File Offset: 0x0000057B
			[Browsable(false)]
			[EditorBrowsable(EditorBrowsableState.Never)]
			[Obsolete("禁用该属性！", true)]
			public new bool ShowInTaskbar
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			// (get) Token: 0x0600022C RID: 556 RVA: 0x0001D3C0 File Offset: 0x0001B5C0
			// (set) Token: 0x0600022D RID: 557 RVA: 0x0000237B File Offset: 0x0000057B
			[Browsable(false)]
			[Obsolete("禁用该属性！", true)]
			[EditorBrowsable(EditorBrowsableState.Never)]
			public new FormStartPosition StartPosition
			{
				get
				{
					return FormStartPosition.Manual;
				}
				set
				{
				}
			}

			// (get) Token: 0x0600022E RID: 558 RVA: 0x0001D36C File Offset: 0x0001B56C
			// (set) Token: 0x0600022F RID: 559 RVA: 0x0000237B File Offset: 0x0000057B
			[Browsable(false)]
			[EditorBrowsable(EditorBrowsableState.Never)]
			[Obsolete("禁用该属性！", true)]
			public new bool TopMost
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			// (get) Token: 0x06000230 RID: 560 RVA: 0x0001D3D4 File Offset: 0x0001B5D4
			// (set) Token: 0x06000231 RID: 561 RVA: 0x0000237B File Offset: 0x0000057B
			[Browsable(false)]
			[Obsolete("禁用该属性！", true)]
			[EditorBrowsable(EditorBrowsableState.Never)]
			public new FormWindowState WindowState
			{
				get
				{
					return FormWindowState.Normal;
				}
				set
				{
				}
			}

			public readonly AppMouseMessageHandler _mouseMsgFilter;

			public bool _isShowDialogAgain;

			public BorderStyle _borderType;

			public Border3DStyle _border3DStyle;

			public ButtonBorderStyle _borderSingleStyle;

			public Color _borderColor;

			public class AppMouseMessageHandler : IMessageFilter
			{

				public AppMouseMessageHandler(FloatLayerBase layerForm)
				{
					_layerForm = layerForm;
				}

				public bool PreFilterMessage(ref Message m)
				{
					var flag = m.Msg == 513 && _layerForm.Visible && !NativeMethods.GetWindowRect(_layerForm.Handle).Contains(MousePosition);
					var flag2 = flag;
					var flag3 = flag2;
					var flag4 = flag3;
					var flag5 = flag4;
					var flag6 = flag5;
					var flag7 = flag6;
					var flag8 = flag7;
					var flag9 = flag8;
					var flag10 = flag9;
					if (flag10)
					{
						_layerForm.Hide();
					}
					return false;
				}

				public readonly FloatLayerBase _layerForm;
			}

			public static class NativeMethods
			{

				[DllImport("user32.dll")]
				[return: MarshalAs(UnmanagedType.Bool)]
				public static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

				[DllImport("user32.dll", CharSet = CharSet.Auto)]
				public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

				[DllImport("user32.dll")]
				public static extern bool ReleaseCapture();

				[DllImport("user32.dll", SetLastError = true)]
				public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

				[DllImport("user32.dll", SetLastError = true)]
				public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

				public static Rectangle GetWindowRect(IntPtr hwnd)
				{
					RECT rect;
					GetWindowRect(hwnd, out rect);
					return (Rectangle)rect;
				}

				public struct RECT
				{

					public static explicit operator Rectangle(RECT rect)
					{
						return new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
					}

					public int left;

					public int top;

					public int right;

					public int bottom;
				}
			}
		}

		public class ColorPicker : ToolStripButton
		{

			public ColorPicker()
			{
				cp = new HWColorPicker
				{
					BorderType = BorderStyle.FixedSingle
				};
			}

			protected override void OnClick(EventArgs e)
			{
				var flag = cp.ShowDialog(this) == DialogResult.OK;
				var flag2 = flag;
				var flag3 = flag2;
				var flag4 = flag3;
				var flag5 = flag4;
				var flag6 = flag5;
				var flag7 = flag6;
				var flag8 = flag7;
				var flag9 = flag8;
				var flag10 = flag9;
				if (flag10)
				{
					select_color = cp.SelectedColored;
					base.OnClick(e);
				}
			}

			// (get) Token: 0x0600023D RID: 573 RVA: 0x0001D52C File Offset: 0x0001B72C
			public Color SelectedColor
			{
				get
				{
					return select_color;
				}
			}

			private readonly HWColorPicker cp;

			public Color select_color;
		}

		public class AdvRichTextBox : RichTextBox
		{

			public void BeginUpdate()
			{
				updating++;
				var flag = updating <= 1;
				var flag2 = flag;
				var flag3 = flag2;
				var flag4 = flag3;
				var flag5 = flag4;
				var flag6 = flag5;
				var flag7 = flag6;
				var flag8 = flag7;
				var flag9 = flag8;
				if (flag9)
				{
					oldEventMask = SendMessage(new HandleRef(this, Handle), 1073, 0, 0);
					SendMessage(new HandleRef(this, Handle), 11, 0, 0);
				}
			}

			public void EndUpdate()
			{
				updating--;
				var flag = updating <= 0;
				var flag2 = flag;
				var flag3 = flag2;
				var flag4 = flag3;
				var flag5 = flag4;
				var flag6 = flag5;
				var flag7 = flag6;
				var flag8 = flag7;
				var flag9 = flag8;
				if (flag9)
				{
					SendMessage(new HandleRef(this, Handle), 11, 1, 0);
					SendMessage(new HandleRef(this, Handle), 1073, 0, oldEventMask);
				}
			}

			// (get) Token: 0x06000240 RID: 576 RVA: 0x0001D63C File Offset: 0x0001B83C
			// (set) Token: 0x06000241 RID: 577 RVA: 0x0001D6C4 File Offset: 0x0001B8C4
			public new TextAlign SelectionAlignment
			{
				get
				{
					var paraformat = default(PARAFORMAT);
					paraformat.cbSize = Marshal.SizeOf(paraformat);
					SendMessage(new HandleRef(this, Handle), 1085, 1, ref paraformat);
					var flag = (paraformat.dwMask & 8u) == 0u;
					var flag2 = flag;
					var flag3 = flag2;
					var flag4 = flag3;
					var flag5 = flag4;
					var flag6 = flag5;
					var flag7 = flag6;
					var flag8 = flag7;
					var flag9 = flag8;
					TextAlign result;
					if (flag9)
					{
						result = TextAlign.Left;
					}
					else
					{
						result = (TextAlign)paraformat.wAlignment;
					}
					return result;
				}
				set
				{
					var paraformat = default(PARAFORMAT);
					paraformat.cbSize = Marshal.SizeOf(paraformat);
					paraformat.dwMask = 8u;
					paraformat.wAlignment = (short)value;
					SendMessage(new HandleRef(this, Handle), 1095, 1, ref paraformat);
				}
			}

			protected override void OnHandleCreated(EventArgs e)
			{
				base.OnHandleCreated(e);
				var flag = !AutoWordSelection;
				var flag2 = flag;
				var flag3 = flag2;
				var flag4 = flag3;
				var flag5 = flag4;
				var flag6 = flag5;
				var flag7 = flag6;
				var flag8 = flag7;
				var flag9 = flag8;
				if (flag9)
				{
					AutoWordSelection = true;
					AutoWordSelection = false;
				}
				SendMessage(new HandleRef(this, Handle), 1226, 1, 1);
			}

			[DllImport("user32", CharSet = CharSet.Auto)]
			private static extern int SendMessage(HandleRef hWnd, int msg, int wParam, int lParam);

			[DllImport("user32", CharSet = CharSet.Auto)]
			private static extern int SendMessage(HandleRef hWnd, int msg, int wParam, ref PARAFORMAT lp);

			public void SetLineSpace()
			{
				var paraformat = default(PARAFORMAT);
				paraformat.cbSize = Marshal.SizeOf(paraformat);
				paraformat.bLineSpacingRule = 4;
				paraformat.dyLineSpacing = 400;
				paraformat.dwMask = 256u;
				SendMessage(new HandleRef(this, Handle), 1095, 0, ref paraformat);
			}

			// (set) Token: 0x06000246 RID: 582 RVA: 0x00002E76 File Offset: 0x00001076
			public string SetLine
			{
				set
				{
					SetLineSpace();
				}
			}

			private const int EM_SETEVENTMASK = 1073;

			private const int EM_GETPARAFORMAT = 1085;

			private const int EM_SETPARAFORMAT = 1095;

			private const int EM_SETTYPOGRAPHYOPTIONS = 1226;

			private const int WM_SETREDRAW = 11;

			private const int TO_ADVANCEDTYPOGRAPHY = 1;

			private const int PFM_ALIGNMENT = 8;

			private const int SCF_SELECTION = 1;

			private int updating;

			private int oldEventMask;

			private struct PARAFORMAT
			{

				public int cbSize;

				public uint dwMask;

				public short wNumbering;

				public short wReserved;

				public int dxStartIndent;

				public int dxRightIndent;

				public int dxOffset;

				public short wAlignment;

				public short cTabCount;

				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
				public int[] rgxTabs;

				public int dySpaceBefore;

				public int dySpaceAfter;

				public int dyLineSpacing;

				public short sStyle;

				public byte bLineSpacingRule;

				public byte bOutlineLevel;

				public short wShadingWeight;

				public short wShadingStyle;

				public short wNumberingStart;

				public short wNumberingStyle;

				public short wNumberingTab;

				public short wBorderSpace;

				public short wBorderWidth;

				public short wBorders;
			}
		}

		public enum TextAlign
		{

			Left = 1,

			Right,

			Center,

			Justify
		}
	}
}