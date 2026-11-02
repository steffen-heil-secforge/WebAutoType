using System;
using System.Linq;
using System.Windows.Forms;
using Accessibility;

namespace WebAutoType
{
	internal class InternetExplorerBrowserUrlReader : BrowserUrlReader
	{
		public InternetExplorerBrowserUrlReader(IntPtr hwnd) : base(hwnd)
		{
		}

		protected override IAccessible GetDocument()
		{
			var ieServerHwnd = FindDescendantWindows(mHwnd, "Internet Explorer_Server").FirstOrDefault();
			if (ieServerHwnd == default(IntPtr))
			{
				return null;
			}
			var ieServer = AccessibleObjectHelper.GetAccessibleObjectFromWindow(ieServerHwnd);
			return ieServer;
		}

		protected override string GetDocumentUrl(IAccessible document)
		{
			return AccessibleObjectHelper.SafeGetName(document);
		}

		protected override string GetDocumentTitle(IAccessible document)
		{
			var firstChild = AccessibleObjectHelper.GetChildren(document).FirstOrDefault();
			if (firstChild != null)
			{
				return AccessibleObjectHelper.SafeGetName(firstChild);
			}
			return null;
		}

		protected override IAccessible GetElementParentDocument(IAccessible element)
		{
			return AccessibleObjectHelper.FindAncestor(element, AccessibleRole.Client);
		}
	}
}