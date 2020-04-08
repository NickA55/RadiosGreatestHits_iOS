// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace RadiosGreatestHits_iOS
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UIButton btnMute { get; set; }

		[Outlet]
		UIKit.UIButton btnPlay { get; set; }

		[Outlet]
		UIKit.UIButton btnStop { get; set; }

		[Outlet]
		UIKit.UILabel lblNowPlaying1 { get; set; }

		[Action ("btnMute_Click:")]
		partial void btnMute_Click (UIKit.UIButton sender);

		[Action ("btnPlay_Click:")]
		partial void btnPlay_Click (UIKit.UIButton sender);

		[Action ("btnStop_Click:")]
		partial void btnStop_Click (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnPlay != null) {
				btnPlay.Dispose ();
				btnPlay = null;
			}

			if (btnMute != null) {
				btnMute.Dispose ();
				btnMute = null;
			}

			if (btnStop != null) {
				btnStop.Dispose ();
				btnStop = null;
			}

			if (lblNowPlaying1 != null) {
				lblNowPlaying1.Dispose ();
				lblNowPlaying1 = null;
			}
		}
	}
}
