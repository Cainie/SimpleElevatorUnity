using UnityEngine;

public class ElevatorSoundController : MonoBehaviour{
    [SerializeField] private AudioSource elevatorSoundsAudioSource;
    [SerializeField] private AudioSource elevatorMusicAudioSource;
    [SerializeField] private AudioClip elevatorMusicAudioClip;
    [SerializeField] private AudioClip onFloorArrivalAudioClip;
    [SerializeField] private AudioClip onElevatorStartAudioClip;

    private void Awake(){
        elevatorMusicAudioSource.clip = elevatorMusicAudioClip;
    }

    public void PlayOnFloorArrivalSound(){
        elevatorSoundsAudioSource.PlayOneShot(onFloorArrivalAudioClip);
    }

    public void PlayOnElevatorStartSound(){
        elevatorSoundsAudioSource.PlayOneShot(onElevatorStartAudioClip);
    }

    public void StopElevatorMusic(){
        elevatorMusicAudioSource.Stop();
    }
    
    public void StartElevatorMusic(){
        elevatorMusicAudioSource.Play();
    }
}
