using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Klak.Timeline.Midi;
using MidiJack;
using ColorTools;

public class MidiPianoEffects : MonoBehaviour
{
    public MidiOctave octaveFrom = MidiOctave.Minus2;
    public MidiOctave octaveTo = MidiOctave.Plus8;

    //public MidiSignalReceiver midiSignalReceiver;
    public Transform pianoParent;
    public GameObject pianoEffectPrefab;

    private ParticleSystem[] particleSystems;
    private Transform[] cubeTransforms;

    private void Start()
    {
        if((int)octaveFrom < 1)
        {
            octaveFrom = (MidiOctave)1;
        }
        if((int)octaveTo < (int)octaveFrom || (int)octaveTo < 1)
        {
            octaveTo = octaveFrom;
        }

        CreatePiano();
    }

    private void CreatePiano()
    {
        int delta = (int)octaveTo - (int)octaveFrom + 1;
        //Debug.Log((int)octaveFrom + " | " + (int)octaveTo + " | Delta | " + delta);

        particleSystems = new ParticleSystem[delta * 12];
        cubeTransforms = new Transform[delta * 12];
        int index = 0;

        for (int octave = 0; octave < delta; octave++)
        {
            float octaveRatio = octave * (1f / delta);
            Color currentColor = Color.HSVToRGB(octaveRatio, 1, 1);
            Debug.Log("Octave loop | " + octave + " | Delta " + delta + " | ratio " + octaveRatio);
            for(int note = 1; note < 13; note++)
            {

                //Debug.Log("Note loop");
                GameObject noteEffect = Instantiate(pianoEffectPrefab, pianoParent, false);
                noteEffect.transform.localPosition = new Vector3(index, 0, 0);
                noteEffect.name = (MidiOctave)octave + " | " + (MidiNote)note;
                cubeTransforms[index] = noteEffect.transform.Find("Cube");

                Renderer r = noteEffect.transform.Find("Cube").GetComponent<Renderer>();

                if(noteEffect.name.Contains("Sharp"))
                {
                    r.material.SetColor("_UnlitColor", ColorModifiers.ColorShift(currentColor, 0, 0, -0.25f));
                }
                else
                {
                    r.material.SetColor("_UnlitColor", currentColor);
                }

                //midiSignalReceiver.noteOnEvent.AddListener(delegate { EmissionOn(index); });
                //midiSignalReceiver.noteOffEvent.AddListener(delegate { EmissionOff(index); });

                ParticleSystem ps = noteEffect.transform.Find("Particle System").GetComponent<ParticleSystem>();
                ParticleSystem.MainModule mm = ps.main;
                mm.startColor = currentColor;
                particleSystems[index] = ps;
                index++;
            }
        }
    }

    private void EmissionOn(int index, float velocity)
    {
        ParticleSystem.EmissionModule em = particleSystems[index].emission;
        em.rateOverTime = 100f * velocity;
        ParticleSystem.MainModule mm = particleSystems[index].main;
        mm.startSpeed = new ParticleSystem.MinMaxCurve(170 * velocity, 190 * velocity);
        mm.startLifetime = new ParticleSystem.MinMaxCurve(3f * velocity,5f * velocity);

        cubeTransforms[index].localPosition = new Vector3(0f,-1f,0f);
    }

    private void EmissionOff(int index)
    {
        ParticleSystem.EmissionModule em = particleSystems[index].emission;
        em.rateOverTime = 0f;

        cubeTransforms[index].localPosition = new Vector3(0f, -0.5f, 0f);
    }

    void NoteOn(MidiChannel channel, int note, float velocity)
    {
        Debug.Log("NoteOn: " + channel + "," + note + "," + velocity);
        EmissionOn(note - 36, velocity);
    }

    void NoteOff(MidiChannel channel, int note)
    {
        Debug.Log("NoteOff: " + channel + "," + note);
        EmissionOff(note - 36);
    }

    void OnEnable()
    {
        MidiMaster.noteOnDelegate += NoteOn;
        MidiMaster.noteOffDelegate += NoteOff;
    }

    void OnDisable()
    {
        MidiMaster.noteOnDelegate -= NoteOn;
        MidiMaster.noteOffDelegate -= NoteOff;
    }
}
