using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace KotrakTest.Droid
{
    [Activity(Label = "Details", ParentActivity = typeof(MainActivity))]
    [MetaData("android.support.PARENT_ACTIVITY", Value = ".MainActivity")]
    public class BrowseItemDetailActivity : BaseActivity
    {
        protected override int LayoutResource => Resource.Layout.activity_item_details;

        ItemDetailViewModel viewModel;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var data = Intent.GetStringExtra("data");

            var item = Newtonsoft.Json.JsonConvert.DeserializeObject<Person>(data);
            viewModel = new ItemDetailViewModel(item);

            FindViewById<TextView>(Resource.Id.description).Text = item.Surname;

            SupportActionBar.Title = item.Name;
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnStop()
        {
            base.OnStop();
        }
    }
}
