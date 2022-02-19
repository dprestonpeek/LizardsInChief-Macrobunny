using UnityEngine;
using System;

using UnityEngine.InputSystem;
#if UNITY_PS4
using UnityEngine.PS4;
#endif

public class GamePad : MonoBehaviour
{
    // Custom class for holding all the gamepad sprites
    //[Serializable]
    //public class PS4GamePad
    //{
    //    public SpriteRenderer thumbstickLeft;
    //    public SpriteRenderer thumbstickRight;

    //    public SpriteRenderer cross;
    //    public SpriteRenderer circle;
    //    public SpriteRenderer triangle;
    //    public SpriteRenderer square;

    //    public SpriteRenderer dpadDown;
    //    public SpriteRenderer dpadRight;
    //    public SpriteRenderer dpadUp;
    //    public SpriteRenderer dpadLeft;

    //    public SpriteRenderer L1;
    //    public SpriteRenderer L2;
    //    public SpriteRenderer R1;
    //    public SpriteRenderer R2;

    //    public SpriteRenderer lightbar;
    //    public SpriteRenderer options;
    //    public SpriteRenderer speaker;
    //    public SpriteRenderer touchpad;
    //    public Transform gyro;
    //    public TextMesh text;
    //    public Light light;
    //}
    //public PS4GamePad gamePad;

    public int playerId = -1;
    public Transform[] touches;
    public Color inputOn = Color.white;
    public Color inputOff = Color.grey;

#if UNITY_PS4
    int m_StickId;
    Color m_LightbarColour;
    bool m_HasSetupGamepad;
    PS4Input.LoggedInUser m_LoggedInUser;
    PS4Input.ConnectionType m_ConnectionType;

    // Touchpad variables
    int m_TouchNum, m_Touch0X, m_Touch0Y, m_Touch0Id, m_Touch1X, m_Touch1Y, m_Touch1Id;
    int m_TouchResolutionX, m_TouchResolutionY, m_AnalogDeadZoneLeft, m_AnalogDeadZoneRight;
    float m_TouchPixelDensity;

    // Volume sampling variables
    const int k_QSamples = 1024; // array size
    float m_RmsValue; // sound level - RMS
    float[] m_Samples = new float[1024]; // audio samples

    void Start()
    {
        // Stick ID is the player ID + 1
        m_StickId = playerId + 1;

        ToggleGamePad(false);
    }

    void Update()
    {
#if UNITY_EDITOR
        if (PS4Input.PadIsConnected(playerId))
        {
            // Set the gamepad to the start values for the player
            if (!m_HasSetupGamepad)
                ToggleGamePad(true);

            // Handle each part individually
            Touchpad();
            Thumbsticks();
            InputButtons();
            DPadButtons();
            TriggerShoulderButtons();
            Lightbar();
            Speaker();

            // Options button is on its own, so we'll do it here
            //if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + m_StickId + "Button7", true)))
            //{
            if (Gamepad.current.startButton.isPressed)
            {

                //gamePad.options.color = inputOn;

                // Reset the gyro orientation and lightbar to default
                PS4Input.PadResetOrientation(playerId);
                PS4Input.PadResetLightBar(playerId);
                GameInput.startButton = true;
                m_LightbarColour = GetPlayerColor(PS4Input.GetUsersDetails(playerId).color);
            }
            else
                GameInput.startButton = false;
            //gamePad.options.color = inputOff;

            // Make the gyro rotate to match the physical controller
            //gamePad.gyro.localEulerAngles = new Vector3(-PS4Input.PadGetLastOrientation(playerId).x,
            //                                            -PS4Input.PadGetLastOrientation(playerId).y,
            //                                            PS4Input.PadGetLastOrientation(playerId).z) * 100;

            // rebuild the username everyframe, in case it's changed due to PSN access
            //gamePad.text.text = PS4Input.RefreshUsersDetails(playerId).userName + "\n(" + m_ConnectionType + ")";

        }
        else if (m_HasSetupGamepad)
            ToggleGamePad(false);
#endif
    }

    // Toggle the gamepad between connected and disconnected states
    void ToggleGamePad(bool active)
    {
        if (active)
        {
            // Set the lightbar colour to the start/default value
            m_LightbarColour = GetPlayerColor(PS4Input.GetUsersDetails(playerId).color);

            // Set 3D Text to whoever's using the pad
            m_LoggedInUser = PS4Input.RefreshUsersDetails(playerId);
            //gamePad.text.text = m_LoggedInUser.userName + "\n(" + m_ConnectionType + ")";

            // Reset and show the gyro
            //gamePad.gyro.localRotation = Quaternion.identity;
            //gamePad.gyro.gameObject.SetActive(true);

            m_HasSetupGamepad = true;
        }
        else
        {
            // Hide the touches
            touches[0].gameObject.SetActive(false);
            touches[1].gameObject.SetActive(false);

            // Set the lightbar to a default colour
            m_LightbarColour = Color.gray;
            //gamePad.lightbar.color = m_LightbarColour;
            //gamePad.light.color = Color.black;

            // Set the 3D Text to show the pad is disconnected
            //gamePad.text.text = "Disconnected";

            // Hide the gyro
            //gamePad.gyro.gameObject.SetActive(false);

            m_HasSetupGamepad = false;
        }
    }

    void Touchpad()
    {
        PS4Input.GetPadControllerInformation(playerId, out m_TouchPixelDensity, out m_TouchResolutionX, out m_TouchResolutionY, out m_AnalogDeadZoneLeft, out m_AnalogDeadZoneRight, out m_ConnectionType);
        PS4Input.GetLastTouchData(playerId, out m_TouchNum, out m_Touch0X, out m_Touch0Y, out m_Touch0Id, out m_Touch1X, out m_Touch1Y, out m_Touch1Id);

        // Show and move around up to 2 touch inputs
        if (m_TouchNum > 0)
        {
            float xPos;
            float yPos;

            // Touch 1
            //if (m_Touch0X > 0 || m_Touch0Y > 0)
            //{
            //    if (!touches[0].gameObject.activeSelf)
            //        touches[0].gameObject.SetActive(true);

            //    xPos = (3.57f / m_TouchResolutionX) * m_Touch0X;
            //    yPos = (1.35f / m_TouchResolutionY) * m_Touch0Y;

            //    touches[0].localPosition = new Vector3(xPos, -yPos, 1);
            //}
            //else if (touches[0].gameObject.activeSelf)
            //    touches[0].gameObject.SetActive(false);

            //Touch 2
            //if (m_TouchNum > 1 && (m_Touch1X > 0 || m_Touch1Y > 0))
            //{
            //    if (!touches[1].gameObject.activeSelf)
            //        touches[1].gameObject.SetActive(true);

            //    xPos = (3.57f / m_TouchResolutionX) * m_Touch1X;
            //    yPos = (1.35f / m_TouchResolutionY) * m_Touch1Y;

            //    touches[1].localPosition = new Vector3(xPos, -yPos, 1);
            //}
            //else if (touches[1].gameObject.activeSelf)
            //    touches[1].gameObject.SetActive(false);
        }
        else if (touches[0].gameObject.activeSelf || touches[1].gameObject.activeSelf)
        {
            //touches[0].gameObject.SetActive(false);
            //touches[1].gameObject.SetActive(false);
        }

        // Make the touchpad light up and play audio if it's pushed down
        //if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + m_StickId + "Button6", true)))
        //{
        if (Gamepad.current.selectButton.isPressed)
        {

            //gamePad.touchpad.color = inputOn;
            GameInput.selectButton = true;
            //TouchpadAudio(m_TouchResolutionX, m_TouchResolutionY, m_Touch0X, m_Touch0Y);
        }
        else
        {
            //gamePad.touchpad.color = inputOff;
            GameInput.selectButton = false;
            //GetComponent<AudioSource>().Stop();
        }
    }

    // Change the pitch and volume of an audio source, via the inputs of 
    // the touchpad, and play it through the controller speaker
    void TouchpadAudio(int maxX, int maxY, int posX, int posY)
    {
        //var touchInput = new Rect { width = maxX, height = maxY, x = posX, y = posY };

        //var xMod = touchInput.x / touchInput.width;
        //var yMod = touchInput.y / touchInput.height;

        //GetComponent<AudioSource>().pitch = xMod + 0.5f;
        //GetComponent<AudioSource>().volume = 1f - yMod;

        //if (!GetComponent<AudioSource>().isPlaying)
        //    GetComponent<AudioSource>().PlayOnDualShock4(m_LoggedInUser.userId);
    }

    void Thumbsticks()
    {
        // Move the thumbsticks around
        GameInput.lJoystick = Gamepad.current.leftStick.ReadValue();
        GameInput.rJoystick = Gamepad.current.rightStick.ReadValue();

        // Make the thumbsticks light up when pressed
        GameInput.lStick = Gamepad.current.leftStickButton.isPressed;
        GameInput.rStick = Gamepad.current.rightStickButton.isPressed;
    }

    // Make the Cross, Circle, Triangle and Square buttons light up when pressed
    void InputButtons()
    {
        GameInput.cross = Gamepad.current.crossButton.isPressed;
        GameInput.circle = Gamepad.current.circleButton.isPressed;
        GameInput.square = Gamepad.current.squareButton.isPressed;
        GameInput.triangle = Gamepad.current.triangleButton.isPressed;
    }

    // Make the DPad directions light up when pressed
    void DPadButtons()
    {
        GameInput.dPad = Gamepad.current.dpad.ReadValue();
    }

    void TriggerShoulderButtons()
    {
        GameInput.lTrigger = Gamepad.current.leftTrigger.ReadValue();
        GameInput.rTrigger = Gamepad.current.rightTrigger.ReadValue();

        GameInput.lShoulder = Gamepad.current.leftShoulder.isPressed;
        GameInput.rShoulder = Gamepad.current.rightShoulder.isPressed;
    }

    void Lightbar()
    {
        // Make the lightbar change colour when we hold down buttons
        if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + m_StickId + "Button0", true)))
            m_LightbarColour = Color.Lerp(m_LightbarColour, Color.blue, Time.deltaTime * 4f);

        if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + m_StickId + "Button1", true)))
            m_LightbarColour = Color.Lerp(m_LightbarColour, Color.red, Time.deltaTime * 4f);

        if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + m_StickId + "Button2", true)))
            m_LightbarColour = Color.Lerp(m_LightbarColour, Color.magenta, Time.deltaTime * 4f);

        if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + m_StickId + "Button3", true)))
            m_LightbarColour = Color.Lerp(m_LightbarColour, Color.green, Time.deltaTime * 4f);

        // Set the lightbar sprite and the physical lightbar change to the current colour
        gamePad.lightbar.color = m_LightbarColour;
        gamePad.light.color = m_LightbarColour;
        PS4Input.PadSetLightBar(playerId,
                                Mathf.RoundToInt(m_LightbarColour.r * 255),
                                Mathf.RoundToInt(m_LightbarColour.g * 255),
                                Mathf.RoundToInt(m_LightbarColour.b * 255));
    }

    // Get the volume being played in-game, and make the speaker light up based on the volume
    void Speaker()
    {
        GetVolume();
        gamePad.speaker.color = (Color.white * m_RmsValue) + (Color.white * 0.25f);
    }

    // Get a usable Color from an int
    static Color GetPlayerColor(int colorId)
    {
        switch (colorId)
        {
            case 0:
                return Color.blue;
            case 1:
                return Color.red;
            case 2:
                return Color.green;
            case 3:
                return Color.magenta;
            default:
                return Color.black;
        }
    }

    //Get the volume from an attached audio source component
    void GetVolume()
    {
        if (GetComponent<AudioSource>().time > 0f)
        {
            GetComponent<AudioSource>().GetOutputData(m_Samples, 0); // fill array with samples
            int i;
            var sum = 0f;

            for (i = 0; i < k_QSamples; i++)
                sum += m_Samples[i] * m_Samples[i]; // sum squared samples

            m_RmsValue = Mathf.Sqrt(sum / k_QSamples); // rms = square root of average

            m_RmsValue *= GetComponent<AudioSource>().volume;
        }
        else
            m_RmsValue = 0f;
    }
#endif
}
