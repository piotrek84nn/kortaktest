using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace KotrakTest.Droid
{
    [Activity(Label = "@string/app_name", Icon = "@mipmap/icon",
        LaunchMode = LaunchMode.SingleInstance,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : BaseActivity
    {
        protected override int LayoutResource => Resource.Layout.activity_main;

        private RecyclerView mRecyclerView;
        private BrowseItemsAdapter mAdapter;
        private RecyclerView.LayoutManager mLayoutManager;
        private SwipeRefreshLayout refresher;
        private ProgressBar progress;
        public ItemsViewModel ViewModel { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ViewModel = new ItemsViewModel();

            mRecyclerView = (RecyclerView)FindViewById(Resource.Id.recyclerViewActivity);
            mRecyclerView.HasFixedSize = true;

            mLayoutManager = new LinearLayoutManager(this);
            mRecyclerView.SetLayoutManager(mLayoutManager);

            mAdapter = new BrowseItemsAdapter(this, ViewModel);
            mRecyclerView.SetAdapter(mAdapter);
            
            Toolbar.MenuItemClick += (sender, e) =>
            {
                var intent = new Intent(this, typeof(AddItemActivity)); ;
                StartActivity(intent);
            };

            SupportActionBar.SetDisplayHomeAsUpEnabled(false);
            SupportActionBar.SetHomeButtonEnabled(false);

            refresher = FindViewById<SwipeRefreshLayout>(Resource.Id.refresherActivity);
            refresher.SetColorSchemeColors(Resource.Color.accent);

            progress = FindViewById<ProgressBar>(Resource.Id.progressbar_loadingActivity);
            progress.Visibility = ViewStates.Gone;
        }

        protected override void OnStart()
        {
            base.OnStart();

            refresher.Refresh += Refresher_Refresh;
            mAdapter.ItemClick += Adapter_ItemClick;

            if (ViewModel.Items.Count == 0)
                ViewModel.LoadItemsCommand.Execute(null);
        }

        protected override void OnStop()
        {
            base.OnStop();
            refresher.Refresh -= Refresher_Refresh;
            mAdapter.ItemClick -= Adapter_ItemClick;
        }

        public void Adapter_ItemClick(object sender, RecyclerClickEventArgs e)
        {
            var item = ViewModel.Items[e.Position];
            var intent = new Intent((Activity)this, typeof(BrowseItemDetailActivity));

            intent.PutExtra("data", Newtonsoft.Json.JsonConvert.SerializeObject(item));
            StartActivity(intent);
        }

        void Refresher_Refresh(object sender, EventArgs e)
        {
            ViewModel.LoadItemsCommand.Execute(null);
            refresher.Refreshing = false;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.top_menus, menu);
            return base.OnCreateOptionsMenu(menu);
        }
    }
}
