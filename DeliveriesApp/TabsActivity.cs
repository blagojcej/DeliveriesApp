using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;

namespace DeliveriesApp
{
    [Activity(Label = "TabsActivity")]
    public class TabsActivity : Android.Support.V4.App.FragmentActivity //To support adding fragments
    {
        private TabLayout tabLayout;
        private Android.Support.V7.Widget.Toolbar tabsToolbar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.tabs);

            tabLayout = FindViewById<TabLayout>(Resource.Id.mainTabLayout);
            tabLayout.TabSelected += TableLayout_TabSelected;

            tabsToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.tabsToolbar);

            //Set the menu for toolbar
            tabsToolbar.InflateMenu(Resource.Menu.tabs_menu);
            tabsToolbar.MenuItemClick += TabsToolbar_MenuItemClick;

            //Navigate to default fragment
            FragmentNavigate(new DeliveriesFragment());
        }

        private void TabsToolbar_MenuItemClick(object sender, Android.Support.V7.Widget.Toolbar.MenuItemClickEventArgs e)
        {
            switch (e.Item.ItemId)
            {
                case Resource.Id.action_add:
                {
                        StartActivity(typeof(NewDeliveryActivity));
                }
                    break;
            }
        }

        private void TableLayout_TabSelected(object sender, TabLayout.TabSelectedEventArgs e)
        {
            switch (e.Tab.Position)
            {
                case 0:
                {
                    FragmentNavigate(new DeliveriesFragment());
                }
                    break;
                case 1:
                {
                    FragmentNavigate(new DeliveredFragment());
                }
                    break;
                case 2:
                {
                        FragmentNavigate(new ProfileFragment());
                }
                    break;
            }
        }

        private void FragmentNavigate(Android.Support.V4.App.Fragment fragment)
        {
            var transaction = SupportFragmentManager.BeginTransaction();

            transaction.Replace(Resource.Id.contentFrame, fragment);

            transaction.Commit();
        }
    }
}