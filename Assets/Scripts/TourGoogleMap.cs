using System.Collections;
using System.Collections.Generic;

using Google.Maps.Coord;
using Google.Maps.Event;
using Google.Maps.Examples.Shared;
using UnityEngine;

//static map option
//url = "https://maps.googleapis.com/maps/api/staticmap?center=Brooklyn+Bridge,New+York,NY&zoom=13&size=600x300&maptype=roadmap&markers=color:blue%7Clabel:S%7C40.702147,-74.015794&markers=color:green%7Clabel:G%7C40.711614,-74.012318&markers=color:red%7Clabel:C%7C40.718217,-73.998284&key=AIzaSyBuTO1TPvcKxBfDqa0RXmhcIjJ32Xr-ZOA";
//var www : WWW = new WWW(url);
//yield www;
//renderer.material.mainTexture=www.texture;


namespace Google.Maps.Examples {
  [RequireComponent(typeof(MapsService))]
  public class TourGoogleMap : MonoBehaviour {
    [Tooltip("LatLng to load (must be set before hitting play).")]
    public LatLng LatLng = new LatLng(47.66685234815894, -117.40292486555248);

    private void Start() {
      // Get required MapsService component on this GameObject.
      MapsService mapsService = GetComponent<MapsService>();

      // Set real-world location to load.
      mapsService.InitFloatingOrigin(LatLng);

      // Register a listener to be notified when the map is loaded.
      mapsService.Events.MapEvents.Loaded.AddListener(OnLoaded);

      // Load map with default options.
      mapsService.LoadMap(ExampleDefaults.DefaultBounds, ExampleDefaults.DefaultGameObjectOptions);
    }

    /// <summary>
    /// Example of OnLoaded event listener.
    /// </summary>
    /// <remarks>
    /// The communication between the game and the MapsSDK is done through APIs and event listeners.
    /// </remarks>
    public void OnLoaded(MapLoadedArgs args) {
      // The Map is loaded - you can start/resume gameplay from that point.
      // The new geometry is added under the GameObject that has MapsService as a component.
    }
  }
}

