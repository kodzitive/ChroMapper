﻿using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsSettings : MonoBehaviour
{
    #region General
    [Header("Toggles"), Header("-------- General --------")]
    [SerializeField] private BetterToggle discordToggle;
    [SerializeField] private BetterToggle darkThemeToggle;
    
    [Header("Sliders")]
    [SerializeField] private BetterSlider initialBatchSizeSlider;
    
    [Header("Misc")]
    [SerializeField] private TextMeshProUGUI installFieldErrorText;
    
    [SerializeField] private BetterInputField customLevelField;
    [SerializeField] private BetterInputField autoSaveInterval;

    #endregion
    
    #region Mapping
    [Header("Toggles"), Header("-------- Mapping --------")]
    [SerializeField] private BetterToggle oscEnabled;
    [SerializeField] private BetterToggle countersPlus;
    [SerializeField] private BetterToggle chromaOnly;
    [SerializeField] private BetterToggle boxSelect;
    [SerializeField] private BetterToggle perfectWalls; // Dont place 0 walls
    [SerializeField, Tooltip("360/90 Mode")] private BetterToggle rotateTrack; // 360/90 mode
    [SerializeField] private BetterToggle highlightRecentlyPlaced;

    [Header("Sliders")]
    [SerializeField] private BetterSlider songSpeedSlider;

    [Header("Misc")]
    [SerializeField] private BetterInputField noteLanes;
    [SerializeField] private TMP_InputField oscIP;
    [SerializeField] private TMP_InputField oscPort;
    
    #endregion
    
   
    
    #region Audio
    [Header("Toggles"), Header("-------- Audio --------")]
    [SerializeField] private BetterToggle redNoteDing;
    [SerializeField] private BetterToggle blueNoteDing;
    [SerializeField] private BetterToggle bombDing;
    
    [Header("Sliders")]
    [SerializeField] private VolumeSlider volumeSlider;
    [SerializeField] private VolumeSlider metronomeSlider;
    [SerializeField] private VolumeSlider noteHitVolumeSlider;

    [Header("Misc")]
    [SerializeField] private TMP_Dropdown noteHitSoundDropdown;
    
    #endregion
    
    #region Graphics
    [Header("Toggles"), Header("-------- Graphics --------")]
    [SerializeField] private BetterToggle chromaLite;
    [SerializeField] private BetterToggle chroma;
    [SerializeField] private BetterToggle waveformGenerator;
    
    [Header("Sliders")]
    [SerializeField] private BetterSlider pastNotesGridScaleSlider;
    [SerializeField] private BetterSlider cameraFOVSlider;
    [SerializeField] private BetterSlider editorScaleSlider;
    [SerializeField] private BetterSlider chunkDistanceSlider;
    [SerializeField] private BetterSlider spawnOffset;
    [SerializeField] private BetterSlider despawnOffset;
    
    //[Header("Misc")]
    
    #endregion
    
    #region Controls
    [Header("Toggles"), Header("-------- Controls --------")]
    [SerializeField] private BetterToggle invertPrecisionScroll;
    [SerializeField] private BetterToggle waveformWorkflow;
    [SerializeField] private BetterToggle nodeEditor;
    [SerializeField] private BetterToggle nodeEditorKeybind;
    [SerializeField] private BetterToggle invertControls;
    
    [Header("Sliders")]
    [SerializeField] private BetterSlider mouseSensSlider;
    [SerializeField] private BetterSlider cameraSpeedSlider;
    
    //[Header("Misc")]
    
    #endregion
    
    private MetronomeHandler _metronomeHandler;

    private void Awake()
    {
        /*
        oscIP.text = Settings.Instance.OSC_IP;
        oscPort.text = Settings.Instance.OSC_Port;
        oscEnabled.Set(Settings.Instance.OSC_Enabled);
       */
        
        #region General

        discordToggle.Set(Settings.Instance.DiscordRPCEnabled);
        darkThemeToggle.Set(Settings.Instance.DarkTheme);
        
        customLevelField.Set(Settings.Instance.BeatSaberInstallation);
        initialBatchSizeSlider.Set(Settings.Instance.InitialLoadBatchSize / 50);
        #endregion
    
        #region Mapping
        countersPlus.Set(Settings.Instance.CountersPlus);
        chromaOnly.Set(Settings.Instance.PlaceOnlyChromaEvents);
        boxSelect.Set(Settings.Instance.BoxSelect);
        perfectWalls.Set(Settings.Instance.DontPlacePerfectZeroDurationWalls);
        rotateTrack.Set(Settings.Instance.RotateTrack);
        
        songSpeedSlider.Set(OptionsController.Find<SongSpeedController>()?.source.pitch * 10f ?? 10f);
        editorScaleSlider.Set(Settings.Instance.EditorScale);
        
        autoSaveInterval.Set(Settings.Instance.AutoSaveInterval.ToString());
        noteLanes.Set(OptionsController.Find<NoteLanesController>()?.NoteLanes.ToString() ?? "4");
        #endregion
        
        #region Audio
        _metronomeHandler = OptionsController.Find<MetronomeHandler>();
        volumeSlider.Set(Settings.Instance.Volume);
        metronomeSlider.Set(Settings.Instance.MetronomeVolume);
        noteHitVolumeSlider.Set(Settings.Instance.NoteHitVolume) ;

        redNoteDing.Set(DingOnNotePassingGrid.NoteTypeToDing[BeatmapNote.NOTE_TYPE_A]);
        blueNoteDing.Set(DingOnNotePassingGrid.NoteTypeToDing[BeatmapNote.NOTE_TYPE_B]);
        bombDing.Set(DingOnNotePassingGrid.NoteTypeToDing[BeatmapNote.NOTE_TYPE_BOMB]);
        
        noteHitSoundDropdown.value = Settings.Instance.NoteHitSound;
        #endregion
    
        #region Graphics
        chromaLite.Set(Settings.Instance.EmulateChromaLite);
        chroma.Set(Settings.Instance.EmulateChromaAdvanced);
        pastNotesGridScaleSlider.Set(Settings.Instance.PastNotesGridScale * 10);
        
        waveformGenerator.Set(Settings.Instance.WaveformGenerator);
        waveformWorkflow.Set(Settings.Instance.WaveformWorkflow);
        highlightRecentlyPlaced.Set(Settings.Instance.HighlightLastPlacedNotes);
        
        cameraFOVSlider.Set(Settings.Instance.CameraFOV);
        chunkDistanceSlider.Set(Settings.Instance.ChunkDistance);
        spawnOffset.Set(Settings.Instance.Offset_Spawning);
        despawnOffset.Set(Settings.Instance.Offset_Despawning);
        #endregion
    
        #region Controls
        nodeEditor.Set(Settings.Instance.NodeEditor_Enabled);
        nodeEditorKeybind.Set(Settings.Instance.NodeEditor_UseKeybind);
        invertControls.Set(Settings.Instance.InvertNoteControls);
        invertPrecisionScroll.Set(Settings.Instance.InvertPrecisionScroll);
        
        mouseSensSlider.Set(Mathf.RoundToInt((Settings.Instance.Camera_MouseSensitivity - 0.5f) * 2));
        cameraSpeedSlider.Set(Settings.Instance.Camera_MovementSpeed);
        #endregion
        
    }
    
    #region General
    public void UpdateDiscordRPC(bool enable)
    {
        if (Settings.Instance.DiscordRPCEnabled == enable) return;
        Settings.Instance.DiscordRPCEnabled = enable;
        PersistentUI.Instance.ShowDialogBox("A restart is required for changes to apply.", null,
            PersistentUI.DialogBoxPresetType.Ok);
    }

    public void UpdateBeatSaberInstall(string value)
    {
        string old = Settings.Instance.BeatSaberInstallation;
        Settings.Instance.BeatSaberInstallation = value;
        installFieldErrorText.text = "All good!";
        if (!Settings.ValidateDirectory(ErrorFeedback))
            Settings.Instance.BeatSaberInstallation = old;
    }

    private void ErrorFeedback(string feedback)
    {
        installFieldErrorText.text = feedback;
    }

    public void UpdateInitialBatchSize(float value)
    {
        int batchSize = Mathf.RoundToInt(value * 50);
        Settings.Instance.InitialLoadBatchSize = batchSize;
    }

    public void UpdateDarkTheme(bool enable)
    {
        if (enable == Settings.Instance.DarkTheme) return;
        PersistentUI.Instance.ShowDialogBox("A restart may be required for all changes to apply.", null,
            PersistentUI.DialogBoxPresetType.Ok);
        Settings.Instance.DarkTheme = enable;
    }
    
    #endregion
    
    
    public void UpdateEditorScale(float scale)
    {
        Settings.Instance.EditorScale = Mathf.RoundToInt(scale);
        OptionsController.Find<EditorScaleController>()?.UpdateEditorScale(scale);
    }

    public void UpdateSongSpeed(float speed)
    {
        OptionsController.Find<SongSpeedController>()?.UpdateSongSpeed(speed);
    }

    public void ToggleAutoSave(bool enabled)
    {
        OptionsController.Find<AutoSaveController>()?.ToggleAutoSave(enabled);
    }

    public void UpdateAutoSaveInterval(string value)
    {
        if (int.TryParse(value, out int interval) && interval > 0)
            Settings.Instance.AutoSaveInterval = interval;
    }

    public void UpdateNoteLanes(string value)
    {
        OptionsController.Find<NoteLanesController>()?.UpdateNoteLanes(value);
    }

    public void UpdateInvertedControls(bool inverted)
    {
        Settings.Instance.InvertNoteControls = inverted;
    }

    public void UpdateChromaOnly(bool param)
    {
        Settings.Instance.PlaceOnlyChromaEvents = param;
    }

    

    public void UpdateWaveform(bool enabled)
    {
        if (Settings.Instance.WaveformGenerator != enabled)
        {
            switch (enabled) {
                case true:
                PersistentUI.Instance.ShowDialogBox(
                    "If you are in the Editor, please exit and re-enter to see the waveform." +
                    "\n\nThe waveform will take a while to generate, depending on the length of the song." +
                    "\n\nYou will still be able to edit, map, and play while the waveform is generating.",
                    null, PersistentUI.DialogBoxPresetType.Ok);
                    break;
                case false:
                    PersistentUI.Instance.ShowDialogBox(
                        "If you are in the Editor, please exit and re-enter to remove the waveform.", null,
                        PersistentUI.DialogBoxPresetType.Ok);
                    break;
            }
        }
        Settings.Instance.WaveformGenerator = enabled;
    }

    public void UpdateCountersPlus(bool enabled)
    {
        Settings.Instance.CountersPlus = enabled;
        OptionsController.Find<CountersPlusController>()?.ToggleCounters(enabled);
    }



    public void UpdateChunksLoaded(float value)
    {
        Settings.Instance.ChunkDistance = Mathf.RoundToInt(value);
    }

    public void UpdateOSC()
    {
        Settings.Instance.OSC_IP = oscIP.text;
        Settings.Instance.OSC_Port = oscPort.text;
        Settings.Instance.OSC_Enabled = oscEnabled.isOn;
        OptionsController.Find<OSCMessageSender>()?.ReloadOSCStats();
    }

    public void UpdateBoxSelect(bool v)
    {
        Settings.Instance.BoxSelect = v;
    }

    public void UpdatePerfectWalls(bool v)
    {
        Settings.Instance.DontPlacePerfectZeroDurationWalls = v;
    }
    public void UpdateChromaLite(bool enabled)
    {
        if (!enabled) OptionsController.Find<PlatformDescriptor>()?.KillChromaLights();
        Settings.Instance.EmulateChromaLite = enabled;
    }

    public void UpdateChromaAdvanced(bool enabled)
    {
        Settings.Instance.EmulateChromaAdvanced = enabled;
    }

    public void UpdateRotateTrack(bool enabled)
    {
        Settings.Instance.RotateTrack = enabled;
        OptionsController.Find<TracksManager>()?.RefreshTracks();
        if (Settings.Instance.RotateTrack == enabled) return;
        RotationCallbackController callbackController = OptionsController.Find<RotationCallbackController>();
        callbackController?.RotationChangedEvent?.Invoke(false, Settings.Instance.RotateTrack ? 0 : callbackController.Rotation);

        PersistentUI.Instance.ShowDialogBox("If you are in the editor, side effects can happen." +
                                            "\n\nIf the rotation of the track is not aligned, going back to the beginning or reloading the editor should fix it."
            , null, PersistentUI.DialogBoxPresetType.Ok);
    }

    public void UpdateRecentlyPlacedNotes(bool enabled)
    {
        Settings.Instance.HighlightLastPlacedNotes = enabled;
    }

    public void UpdateSpawnOffset(float v)
    {
        Settings.Instance.Offset_Spawning = Mathf.RoundToInt(v);
    }

    public void UpdateDespawnOffset(float v)
    {
        Settings.Instance.Offset_Despawning = Mathf.RoundToInt(v);
    }


    public void UpdatePastNotesGridScale(float value)
    {
        Settings.Instance.PastNotesGridScale = (float) Math.Round(value / 10, 3);
    }

    #region Mapping
    
    #endregion
    
    #region Audio
    public void UpdateRedNoteDing(bool ding)
    {
        DingOnNotePassingGrid.NoteTypeToDing[BeatmapNote.NOTE_TYPE_A] = ding;
    }

    public void UpdateBlueNoteDing(bool ding)
    {
        DingOnNotePassingGrid.NoteTypeToDing[BeatmapNote.NOTE_TYPE_B] = ding;
    }

    public void UpdateBombDing(bool ding)
    {
        DingOnNotePassingGrid.NoteTypeToDing[BeatmapNote.NOTE_TYPE_BOMB] = ding;
    }
    
    public void UpdateNoteHitSound()
    {
        Settings.Instance.NoteHitSound = noteHitSoundDropdown.value;
    }
    public void UpdateNoteHitVolume(float value)
    {
        Settings.Instance.NoteHitVolume = value;
    }
    public void UpdateGameVolume(float value)
    {
        AudioListener.volume = value;
        Settings.Instance.Volume = value;
    }

    public void UpdateMetronomeVolume(float value)
    {
        Settings.Instance.MetronomeVolume = value;
        if (_metronomeHandler != null) _metronomeHandler.CowBell = Input.GetKey(KeyCode.LeftControl);
    }
    #endregion
    
    #region Graphics

    public void UpdateCameraFOV(float value)
    {
        value = (float) Math.Round(value, 3);
        Settings.Instance.CameraFOV = value;
    }

    #endregion
   
    #region Controls
    public void UpdateWaveformWorkflow(bool enabled)
    {
        Settings.Instance.WaveformWorkflow = enabled;
    }
    public void UpdateNodeEditor(bool enabled)
    {
        Settings.Instance.NodeEditor_Enabled = enabled;
    }
    public void UpdateNodeEditorKeybind(bool v)
    {
        Settings.Instance.NodeEditor_UseKeybind = v; 
    }
    public void UpdateMouseSensitivity(float v)
    {
        Settings.Instance.Camera_MouseSensitivity = (v / 2) + 0.5f;
    }
    public void UpdateCameraSpeed(float v)
    {
        Settings.Instance.Camera_MovementSpeed = v;
    }
    public void UpdateInvertPrecisionScroll(bool enabled)
    {
        Settings.Instance.InvertPrecisionScroll = enabled;
    }
    #endregion
    
}
