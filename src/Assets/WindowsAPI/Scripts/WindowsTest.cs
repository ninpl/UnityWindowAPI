//                                  ┌∩┐(◣_◢)┌∩┐                                
//                                                                              \\
// WindowsTest.cs (28/08/2020)													\\
// Autor: Antonio Mateo (.\Moon Antonio)  -  github.com/moonantonio				\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using UnityEngine.EventSystems;
#endregion

namespace Windows.Control
{
	/// <summary>
	/// <para></para>
	/// </summary>
	[AddComponentMenu("Windows/Windows Test")]
	public class WindowsTest : MonoBehaviour, IDragHandler
	{
		#region Publics
		public Vector2Int defaultWindowSize;
		public Vector2Int borderSize;
		#endregion

		#region Privates
		private Vector2 _deltaValue = Vector2.zero;
		private bool _maximized;
        #endregion

        #region API
        public void OnBorderBtnClick()
        {
            if (WindowsControl.framed)
                return;

            WindowsControl.SetFramedWindow();
            WindowsControl.MoveWindowPos(Vector2Int.zero, Screen.width + borderSize.x, Screen.height + borderSize.y); // Compensating the border offset.
        }

        public void OnNoBorderBtnClick()
        {
            if (!WindowsControl.framed)
                return;

            WindowsControl.SetFramelessWindow();
            WindowsControl.MoveWindowPos(Vector2Int.zero, Screen.width - borderSize.x, Screen.height - borderSize.y);
        }

        public void ResetWindowSize()
        {
            WindowsControl.MoveWindowPos(Vector2Int.zero, defaultWindowSize.x, defaultWindowSize.y);
        }

        public void OnCloseBtnClick()
        {
            EventSystem.current.SetSelectedGameObject(null);
            Application.Quit();
        }

        public void OnMinimizeBtnClick()
        {
            EventSystem.current.SetSelectedGameObject(null);
            WindowsControl.MinimizeWindow();
        }

        public void OnMaximizeBtnClick()
        {
            EventSystem.current.SetSelectedGameObject(null);

            if (_maximized)
                WindowsControl.RestoreWindow();
            else
                WindowsControl.MaximizeWindow();

            _maximized = !_maximized;
        }

        public void OnDrag(PointerEventData data)
        {
            if (WindowsControl.framed)
                return;

            _deltaValue += data.delta;
            if (data.dragging)
            {
                WindowsControl.MoveWindowPos(_deltaValue, Screen.width, Screen.height);
            }
        }
        #endregion
    }
}