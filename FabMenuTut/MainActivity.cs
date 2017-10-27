using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using Android.Views;
using System;
using Android.Animation;

namespace FabMenuTut
{
    [Activity(Label = "FabMenuTut", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private static bool isFabOpen;
        private FloatingActionButton fabAirballoon;
        private FloatingActionButton fabCake;
        private FloatingActionButton fabMain;
        private View bgFabMenu;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            fabAirballoon = FindViewById<FloatingActionButton>(Resource.Id.fab_airballoon);
            fabCake = FindViewById<FloatingActionButton>(Resource.Id.fab_cake);
            fabMain = FindViewById<FloatingActionButton>(Resource.Id.fab_main);
            bgFabMenu = FindViewById<View>(Resource.Id.bg_fab_menu);

            fabMain.Click += (o, e) =>
            {
                if (!isFabOpen)
                    ShowFabMenu();
                else
                    CloseFabMenu();
            };

            fabCake.Click += (o, e) =>
            {
                CloseFabMenu();
                Toast.MakeText(this, "Cake!", ToastLength.Short).Show();
            };

            fabAirballoon.Click += (o, e) =>
            {
                CloseFabMenu();
                Toast.MakeText(this, "Airballoon!", ToastLength.Short).Show();
            };

            bgFabMenu.Click += (o, e) => CloseFabMenu();
        }

        private void ShowFabMenu()
        {
            isFabOpen = true;
            fabAirballoon.Visibility = ViewStates.Visible;
            fabCake.Visibility = ViewStates.Visible;
            bgFabMenu.Visibility = ViewStates.Visible;

            fabMain.Animate().Rotation(135f);
            bgFabMenu.Animate().Alpha(1f);
            fabAirballoon.Animate()
                .TranslationY(-Resources.GetDimension(Resource.Dimension.standard_100))
                .Rotation(0f);
            fabCake.Animate()
                .TranslationY(-Resources.GetDimension(Resource.Dimension.standard_55))
                .Rotation(0f);
        }

        private void CloseFabMenu()
        {
            isFabOpen = false;

            fabMain.Animate().Rotation(0f);
            bgFabMenu.Animate().Alpha(0f);
            fabAirballoon.Animate()
                .TranslationY(0f)
                .Rotation(90f);
            fabCake.Animate()
                .TranslationY(0f)
                .Rotation(90f).SetListener(new FabAnimatorListener(bgFabMenu, fabCake, fabAirballoon));
        }

        private class FabAnimatorListener : Java.Lang.Object, Animator.IAnimatorListener
        {
            View[] viewsToHide;

            public FabAnimatorListener(params View[] viewsToHide)
            {
                this.viewsToHide = viewsToHide;
            }

            public void OnAnimationCancel(Animator animation)
            {
            }

            public void OnAnimationEnd(Animator animation)
            {
                if (!isFabOpen)
                    foreach (var view in viewsToHide)
                        view.Visibility = ViewStates.Gone;
            }

            public void OnAnimationRepeat(Animator animation)
            {
            }

            public void OnAnimationStart(Animator animation)
            {
            }
        }
    }
}

