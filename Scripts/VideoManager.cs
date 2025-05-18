using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage rawImage;

    public void PlayVideo(string url)
    {
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = url;
        videoPlayer.Prepare();

        videoPlayer.prepareCompleted += (vp) => {
            rawImage.texture = vp.texture;
            vp.Play();
        };
    }

    public void StopVideo()
    {
        videoPlayer.Stop();
    }
}
