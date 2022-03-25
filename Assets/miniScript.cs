using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;

public class miniScript : MonoBehaviour {

    public KMBombModule Module;
    public KMBombInfo Bomb;
    public KMAudio Audio;

    public Material[] Colors; //0=blacK, 1=Red, 2=Yellow, 3=Blue, 4=White, 5=Green
    public GameObject[] PanelCovers; 
    public GameObject[] ObjectSets; 
    public KMSelectable[] Wires; 
    public GameObject[] WireObjs; //add 5 for uncut variant
    public KMSelectable[] Buttons; 
    public GameObject[] ButtonObjs; 
    public TextMesh[] ButtonTexts; 
    public KMSelectable[] Keypad; 
    public TextMesh[] KeypadTexts; 
    public KMSelectable[] Switches; 
    public GameObject[] SwitchObjs; 
    public KMSelectable DialTip;
    public GameObject DialObj;
    public KMSelectable DialButton;
    public SpriteRenderer[] DialSprites; 
    public Sprite[] DialShapes; //square, star, triangle, oval, circle
    public KMSelectable[] Arrows;
    public GameObject[] ArrowObjs;
    public GameObject Paper;
    public TextMesh PaperNumber;
    //Arrays above are their corresponding objects in READING ORDER on the module.

    string[] order = {"123456", "123465", "123546", "123564", "123645", "123654", "124356", "124365", "124536", "124563", "124635", "124653", "125346", "125364", "125436", "125463", "125634", "125643", "126345", "126354", "126435", "126453", "126534", "126543", "132456", "132465", "132546", "132564", "132645", "132654", "134256", "134265", "134526", "134562", "134625", "134652", "135246", "135264", "135426", "135462", "135624", "135642", "136245", "136254", "136425", "136452", "136524", "136542", "142356", "142365", "142536", "142563", "142635", "142653", "143256", "143265", "143526", "143562", "143625", "143652", "145236", "145263", "145326", "145362", "145623", "145632", "146235", "146253", "146325", "146352", "146523", "146532", "152346", "152364", "152436", "152463", "152634", "152643", "153246", "153264", "153426", "153462", "153624", "153642", "154236", "154263", "154326", "154362", "154623", "154632", "156234", "156243", "156324", "156342", "156423", "156432", "162345", "162354", "162435", "162453", "162534", "162543", "163245", "163254", "163425", "163452", "163524", "163542", "164235", "164253", "164325", "164352", "164523", "164532", "165234", "165243", "165324", "165342", "165423", "165432", "213456", "213465", "213546", "213564", "213645", "213654", "214356", "214365", "214536", "214563", "214635", "214653", "215346", "215364", "215436", "215463", "215634", "215643", "216345", "216354", "216435", "216453", "216534", "216543", "231456", "231465", "231546", "231564", "231645", "231654", "234156", "234165", "234516", "234561", "234615", "234651", "235146", "235164", "235416", "235461", "235614", "235641", "236145", "236154", "236415", "236451", "236514", "236541", "241356", "241365", "241536", "241563", "241635", "241653", "243156", "243165", "243516", "243561", "243615", "243651", "245136", "245163", "245316", "245361", "245613", "245631", "246135", "246153", "246315", "246351", "246513", "246531", "251346", "251364", "251436", "251463", "251634", "251643", "253146", "253164", "253416", "253461", "253614", "253641", "254136", "254163", "254316", "254361", "254613", "254631", "256134", "256143", "256314", "256341", "256413", "256431", "261345", "261354", "261435", "261453", "261534", "261543", "263145", "263154", "263415", "263451", "263514", "263541", "264135", "264153", "264315", "264351", "264513", "264531", "265134", "265143", "265314", "265341", "265413", "265431", "312456", "312465", "312546", "312564", "312645", "312654", "314256", "314265", "314526", "314562", "314625", "314652", "315246", "315264", "315426", "315462", "315624", "315642", "316245", "316254", "316425", "316452", "316524", "316542", "321456", "321465", "321546", "321564", "321645", "321654", "324156", "324165", "324516", "324561", "324615", "324651", "325146", "325164", "325416", "325461", "325614", "325641", "326145", "326154", "326415", "326451", "326514", "326541", "341256", "341265", "341526", "341562", "341625", "341652", "342156", "342165", "342516", "342561", "342615", "342651", "345126", "345162", "345216", "345261", "345612", "345621", "346125", "346152", "346215", "346251", "346512", "346521", "351246", "351264", "351426", "351462", "351624", "351642", "352146", "352164", "352416", "352461", "352614", "352641", "354126", "354162", "354216", "354261", "354612", "354621", "356124", "356142", "356214", "356241", "356412", "356421", "361245", "361254", "361425", "361452", "361524", "361542", "362145", "362154", "362415", "362451", "362514", "362541", "364125", "364152", "364215", "364251", "364512", "364521", "365124", "365142", "365214", "365241", "365412", "365421", "412356", "412365", "412536", "412563", "412635", "412653", "413256", "413265", "413526", "413562", "413625", "413652", "415236", "415263", "415326", "415362", "415623", "415632", "416235", "416253", "416325", "416352", "416523", "416532", "421356", "421365", "421536", "421563", "421635", "421653", "423156", "423165", "423516", "423561", "423615", "423651", "425136", "425163", "425316", "425361", "425613", "425631", "426135", "426153", "426315", "426351", "426513", "426531", "431256", "431265", "431526", "431562", "431625", "431652", "432156", "432165", "432516", "432561", "432615", "432651", "435126", "435162", "435216", "435261", "435612", "435621", "436125", "436152", "436215", "436251", "436512", "436521", "451236", "451263", "451326", "451362", "451623", "451632", "452136", "452163", "452316", "452361", "452613", "452631", "453126", "453162", "453216", "453261", "453612", "453621", "456123", "456132", "456213", "456231", "456312", "456321", "461235", "461253", "461325", "461352", "461523", "461532", "462135", "462153", "462315", "462351", "462513", "462531", "463125", "463152", "463215", "463251", "463512", "463521", "465123", "465132", "465213", "465231", "465312", "465321", "512346", "512364", "512436", "512463", "512634", "512643", "513246", "513264", "513426", "513462", "513624", "513642", "514236", "514263", "514326", "514362", "514623", "514632", "516234", "516243", "516324", "516342", "516423", "516432", "521346", "521364", "521436", "521463", "521634", "521643", "523146", "523164", "523416", "523461", "523614", "523641", "524136", "524163", "524316", "524361", "524613", "524631", "526134", "526143", "526314", "526341", "526413", "526431", "531246", "531264", "531426", "531462", "531624", "531642", "532146", "532164", "532416", "532461", "532614", "532641", "534126", "534162", "534216", "534261", "534612", "534621", "536124", "536142", "536214", "536241", "536412", "536421", "541236", "541263", "541326", "541362", "541623", "541632", "542136", "542163", "542316", "542361", "542613", "542631", "543126", "543162", "543216", "543261", "543612", "543621", "546123", "546132", "546213", "546231", "546312", "546321", "561234", "561243", "561324", "561342", "561423", "561432", "562134", "562143", "562314", "562341", "562413", "562431", "563124", "563142", "563214", "563241", "563412", "563421", "564123", "564132", "564213", "564231", "564312", "564321", "612345", "612354", "612435", "612453", "612534", "612543", "613245", "613254", "613425", "613452", "613524", "613542", "614235", "614253", "614325", "614352", "614523", "614532", "615234", "615243", "615324", "615342", "615423", "615432", "621345", "621354", "621435", "621453", "621534", "621543", "623145", "623154", "623415", "623451", "623514", "623541", "624135", "624153", "624315", "624351", "624513", "624531", "625134", "625143", "625314", "625341", "625413", "625431", "631245", "631254", "631425", "631452", "631524", "631542", "632145", "632154", "632415", "632451", "632514", "632541", "634125", "634152", "634215", "634251", "634512", "634521", "635124", "635142", "635214", "635241", "635412", "635421", "641235", "641253", "641325", "641352", "641523", "641532", "642135", "642153", "642315", "642351", "642513", "642531", "643125", "643152", "643215", "643251", "643512", "643521", "645123", "645132", "645213", "645231", "645312", "645321", "651234", "651243", "651324", "651342", "651423", "651432", "652134", "652143", "652314", "652341", "652413", "652431", "653124", "653142", "653214", "653241", "653412", "653421", "654123", "654132", "654213", "654231", "654312", "654321"};
    string chosenOrder = "";
    string[] ordinals = {"first", "second", "third", "fourth", "fifth", "last"};
    int[] wireColorOrder = {0, 1, 2, 3, 4};
    string[] colorNames = {"black", "red", "yellow", "blue", "white"};
    int[] buttonColorOrder = {0, 1, 2, 3, 4};
    int[] buttonLabelOrder = {0, 1, 2, 3, 4};
    string[] labelNames = {"ABORT", "PRESS", "HOLD", "RELEASE", "DETONATE"};
    string keypadLetters = "ABCDEFGHIJLMNOPQRSTUVWXYZ";
    int[] keypadLetterOrder = {0, 1, 2, 3, 4};
    int[] keypadLetterOffset = {0, 1, 2, 3, 4};
    int[] switchColorOrder = {0, 1, 2, 3, 4};
    int[] switchFlipped = {-1, -1, -1, -1, -1};
    int[] dialShapeOrder = {0, 1, 2, 3, 4};
    string[] shapeNames = {"square", "star", "triangle", "oval", "circle"};
    int dialRotation = -1;
    float[] dialPositions = {-0.0895f, -0.0766f, -0.0667f, -0.065f, -0.0731f, -0.0675f, -0.0692f, -0.0607f, -0.0475f, -0.0373f};
    string[] rotationNames = {"6:00", "4:30", "3:00", "1:30", "12:00"};
    int[] arrowOrder = {0, 1, 2, 3, 4};
    string[] directionNames = {"up", "left", "all", "right", "down"};

    bool moduleOpened = false;
    int stage = 0;
    int previouslyActivePanel = -1;
    ulong number = 1;
    int[] primes = {2, 3, 5, 7, 11, 13};

    //Logging
    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    void Awake () {
        moduleId = moduleIdCounter++;

        Module.GetComponent<KMSelectable>().OnInteract += delegate () { if (!moduleOpened) {OpenFirstPanel();} return true; };

        foreach (KMSelectable wire in Wires) {
            wire.OnInteract += delegate () { WireCut(wire); return false; };
        }
        foreach (KMSelectable button in Buttons) {
            button.OnInteract += delegate () { ButtonPress(button); return false; };
        }
        foreach (KMSelectable key in Keypad) {
            key.OnInteract += delegate () { KeypadPress(key); return false; };
        }
        foreach (KMSelectable swithc in Switches) {
            swithc.OnInteract += delegate () { SwitchToggle(swithc); return false; };
        }
        DialTip.OnInteract += delegate () { DialRotate(); return false; };
        DialButton.OnInteract += delegate () { DialSubmit(); return false; };
        foreach (KMSelectable arrow in Arrows) {
            arrow.OnInteract += delegate () { ArrowPress(arrow); return false; };
        }
    }

    // Use this for initialization
    void Start () {
        Paper.SetActive(false);
        for (int t = 0; t < 6; t++) {
            ObjectSets[t].SetActive(false);
        }
        chosenOrder = order[UnityEngine.Random.Range(0, 720)]; //Chose the order the panels will open in.
        for (int a = 0; a < 6; a++) {
            switch (chosenOrder[a]) {
                case '1': 
                    Debug.LogFormat("[Mini #{0}] The {1} panel is Wires.", moduleId, ordinals[a]); //Setting up wire colors
                    wireColorOrder = wireColorOrder.Shuffle();
                    for (int b = 0; b < 5; b++) {
                        WireObjs[b].GetComponent<MeshRenderer>().material = Colors[wireColorOrder[b]];
                        WireObjs[b+5].GetComponent<MeshRenderer>().material = Colors[wireColorOrder[b]];
                    }
                    Debug.LogFormat("<Mini #{0}> Wire colors: {1}, {2}, {3}, {4}, {5}", moduleId, colorNames[wireColorOrder[0]], colorNames[wireColorOrder[1]], colorNames[wireColorOrder[2]], colorNames[wireColorOrder[3]], colorNames[wireColorOrder[4]]);
                break;

                case '2': 
                    Debug.LogFormat("[Mini #{0}] The {1} panel is Buttons.", moduleId, ordinals[a]); //Setting up button colors and labels
                    buttonColorOrder = buttonColorOrder.Shuffle();
                    for (int c = 0; c < 5; c++) {
                        ButtonObjs[c].GetComponent<MeshRenderer>().material = Colors[buttonColorOrder[c]];
                        if (buttonColorOrder[c] == 0) {
                            ButtonTexts[c].color = new Vector4(1, 1, 1, 1);;
                        }
                    }
                    Debug.LogFormat("[Mini #{0}] Button colors: {1}, {2}, {3}, {4}, {5}", moduleId, colorNames[buttonColorOrder[0]], colorNames[buttonColorOrder[1]], colorNames[buttonColorOrder[2]], colorNames[buttonColorOrder[3]], colorNames[buttonColorOrder[4]]);
                    buttonLabelOrder = buttonLabelOrder.Shuffle();
                    for (int d = 0; d < 5; d++) {
                        ButtonTexts[d].text = labelNames[buttonLabelOrder[d]][0].ToString();
                    }
                    Debug.LogFormat("<Mini #{0}> Button labels: {1}, {2}, {3}, {4}, {5}", moduleId, labelNames[buttonLabelOrder[0]], labelNames[buttonLabelOrder[1]], labelNames[buttonLabelOrder[2]], labelNames[buttonLabelOrder[3]], labelNames[buttonLabelOrder[4]]);
                break;

                case '3': 
                    Debug.LogFormat("[Mini #{0}] The {1} panel is Keypad.", moduleId, ordinals[a]); //Setting up keypad letters
                    keypadLetterOrder = keypadLetterOrder.Shuffle();
                    keypadLetterOffset = keypadLetterOffset.Shuffle();
                    for (int e = 0; e < 5; e++) {
                        KeypadTexts[e].text = keypadLetters[keypadLetterOrder[e]*5+keypadLetterOffset[e]].ToString();
                    }
                    Debug.LogFormat("[Mini #{0}] Keypad labels: {1}, {2}, {3}, {4}, {5}", moduleId, keypadLetters[keypadLetterOrder[0]*5+keypadLetterOffset[0]], keypadLetters[keypadLetterOrder[1]*5+keypadLetterOffset[1]], keypadLetters[keypadLetterOrder[2]*5+keypadLetterOffset[2]], keypadLetters[keypadLetterOrder[3]*5+keypadLetterOffset[3]], keypadLetters[keypadLetterOrder[4]*5+keypadLetterOffset[4]]);
                break;

                case '4': 
                    Debug.LogFormat("[Mini #{0}] The {1} panel is Switches.", moduleId, ordinals[a]); //Setting up switch colors and positions
                    switchColorOrder = switchColorOrder.Shuffle();
                    for (int f = 0; f < 5; f++) {
                        SwitchObjs[f].GetComponent<MeshRenderer>().material = Colors[switchColorOrder[f]];
                        switchFlipped[f] = UnityEngine.Random.Range(0, 2);
                        if (switchFlipped[f] == 1) {
                            SwitchObjs[f].transform.localPosition = new Vector3(0.646f, 0.21f, 0.132f);
                            SwitchObjs[f].transform.localRotation = Quaternion.Euler(-168.638f, -180f, 180f);
                        }
                    }
                    Debug.LogFormat("<Mini #{0}> Switch colors: {1}, {2}, {3}, {4}, {5}", moduleId, colorNames[switchColorOrder[0]], colorNames[switchColorOrder[1]], colorNames[switchColorOrder[2]], colorNames[switchColorOrder[3]], colorNames[switchColorOrder[4]]);
                    Debug.LogFormat("<Mini #{0}> Switch positions: {1}, {2}, {3}, {4}, {5}", moduleId, (switchFlipped[0] == 0) ? "up" : "down", (switchFlipped[1] == 0) ? "up" : "down", (switchFlipped[2] == 0) ? "right" : "left", (switchFlipped[3] == 0) ? "down" : "up", (switchFlipped[4] == 0) ? "down" : "up");
                break;

                case '5': 
                    Debug.LogFormat("[Mini #{0}] The {1} panel is Dial.", moduleId, ordinals[a]); //Setting up dial shapes and initial dial rotation
                    dialShapeOrder = dialShapeOrder.Shuffle();
                    for (int g = 0; g < 5; g++) {
                        DialSprites[g].sprite = DialShapes[dialShapeOrder[g]];
                    }
                    Debug.LogFormat("[Mini #{0}] Dial shapes: {1}, {2}, {3}, {4}, {5}", moduleId, shapeNames[dialShapeOrder[0]], shapeNames[dialShapeOrder[1]], shapeNames[dialShapeOrder[2]], shapeNames[dialShapeOrder[3]], shapeNames[dialShapeOrder[4]]);
                    dialRotation = UnityEngine.Random.Range(0, 5);
                    DialObj.transform.localPosition = new Vector3(dialPositions[dialRotation], 0.0205f, dialPositions[dialRotation+5]);
                    DialObj.transform.localRotation = Quaternion.Euler(270f, 0f, 180f - 45f*dialRotation);
                    Debug.LogFormat("<Mini #{0}> Dial's initial rotation: {1}", moduleId, rotationNames[dialRotation]);
                break;

                case '6': 
                    Debug.LogFormat("[Mini #{0}] The {1} panel is Arrows.", moduleId, ordinals[a]); //Setting up arrow order
                    arrowOrder = arrowOrder.Shuffle();
                    for (int h = 0; h < 5; h++) {
                        switch (h) { //0.013
                            case 0: ArrowObjs[arrowOrder[h]].transform.localPosition += new Vector3(0f, 0f, 0.013f); break;
                            case 1: ArrowObjs[arrowOrder[h]].transform.localPosition += new Vector3(-0.0086f, 0f, 0f); break;
                            case 2: /*no action required*/ break;
                            case 3: ArrowObjs[arrowOrder[h]].transform.localPosition += new Vector3(0.0086f, 0f, 0f); break;
                            case 4: ArrowObjs[arrowOrder[h]].transform.localPosition += new Vector3(0f, 0f, -0.013f); break;
                        }
                    }
                    Debug.LogFormat("[Mini #{0}] Arrow order: {1}, {2}, {3}, {4}, {5}", moduleId, directionNames[arrowOrder[0]], directionNames[arrowOrder[1]], directionNames[arrowOrder[2]], directionNames[arrowOrder[3]], directionNames[arrowOrder[4]]);
                break;
            }
        }
    }

    void OpenFirstPanel() {
        moduleOpened = true;
        switch (chosenOrder[0]) {
            case '1': PanelCovers[0].SetActive(false); ObjectSets[0].SetActive(true); break;
            case '2': PanelCovers[1].SetActive(false); ObjectSets[1].SetActive(true); break;
            case '3': PanelCovers[2].SetActive(false); ObjectSets[2].SetActive(true); break;
            case '4': PanelCovers[3].SetActive(false); ObjectSets[3].SetActive(true); break;
            case '5': PanelCovers[4].SetActive(false); ObjectSets[4].SetActive(true); break;
            case '6': PanelCovers[5].SetActive(false); ObjectSets[5].SetActive(true); break;
        }
        stage = 1;
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.WireSequenceMechanism, transform);
    }

    void WireCut (KMSelectable wire) {
        wire.AddInteractionPunch();
        for (int i = 0; i < 5; i++) {
            if (wire == Wires[i]) {
                Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.WireSnip, wire.transform);
                WireObjs[i].SetActive(false);
                Debug.LogFormat("[Mini #{0}] Stage {1}: You cut wire {2}.", moduleId, stage, i+1);
                CheckValidity(0, i);
            }
        }
    }

    void ButtonPress (KMSelectable button) {
        button.AddInteractionPunch();
        for (int j = 0; j < 5; j++) {
            if (button == Buttons[j]) {
                Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, button.transform);
                Debug.LogFormat("[Mini #{0}] Stage {1}: You pressed the {2} button.", moduleId, stage, colorNames[buttonColorOrder[j]]);
                CheckValidity(1, j);
            }
        }
    }

    void KeypadPress (KMSelectable key) {
        key.AddInteractionPunch();
        for (int k = 0; k < 5; k++) {
            if (key == Keypad[k]) {
                Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, key.transform);
                Debug.LogFormat("[Mini #{0}] Stage {1}: You pressed the key labeled {2}.", moduleId, stage, keypadLetters[keypadLetterOrder[k]*5 + keypadLetterOffset[k]]);
                CheckValidity(2, k);
            }
        }
    }

    void SwitchToggle(KMSelectable swithc) { //misspelled so C# doesn't go insane
        swithc.AddInteractionPunch();
        for (int l = 0; l < 5; l++) {
            if (swithc == Switches[l]) {
                GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.BigButtonPress, swithc.transform);
                switchFlipped[l] = (switchFlipped[l] + 1) % 2;
                if (switchFlipped[l] == 1) {
                    SwitchObjs[l].transform.localPosition = new Vector3(0.646f, 0.21f, 0.132f);
                    SwitchObjs[l].transform.localRotation = Quaternion.Euler(-168.638f, -180f, 180f);
                } else {
                    SwitchObjs[l].transform.localPosition = new Vector3(0.646f, 0.21f, -0.132f);
                    SwitchObjs[l].transform.localRotation = Quaternion.Euler(-168.638f, -0f, 180f);
                }
                string[] s = {"top-left", "top-right", "center", "bottom-left", "bottom-right"};
                Debug.LogFormat("[Mini #{0}] Stage {1}: You toggled the {2} switch.", moduleId, stage, s[l]);
                CheckValidity(3, l);
            }
        }
    }

    void DialRotate() {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.BigButtonPress, DialObj.transform);
        dialRotation = (dialRotation + 4) % 5;
        DialObj.transform.localPosition = new Vector3(dialPositions[dialRotation], 0.0205f, dialPositions[dialRotation+5]);
        DialObj.transform.localRotation = Quaternion.Euler(270f, 0f, 180f - 45f*dialRotation);
    }

    void DialSubmit() {
        DialButton.AddInteractionPunch();
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, DialButton.transform);
        Debug.LogFormat("[Mini #{0}] Stage {1}: You submitted the dial when it was set to {2}.", moduleId, stage, shapeNames[dialShapeOrder[4-dialRotation]]);
        CheckValidity(4, dialRotation);
    }

    void ArrowPress (KMSelectable arrow) {
        arrow.AddInteractionPunch();
        for (int m = 0; m < 5; m++) {
            if (arrow == Arrows[m]) {
                Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, arrow.transform);
                Debug.LogFormat("[Mini #{0}] Stage {1}: You pressed the {2} arrow.", moduleId, stage, directionNames[m]);
                CheckValidity(5, m);
            }
        }
    }

    void CheckValidity (int n, int o) {
        bool p = false; //set to true if you did it correctly
        if (stage == 1) {
            p = true;
        } else {
            switch (n) {
                case 0: 
                    switch (previouslyActivePanel) {
                        case 1: p = (o == 0); break;
                        case 2: p = (o == 4); break;
                        case 3: p = (o == 1); break;
                        case 4: p = (o == 2); break;
                        case 5: p = (o == 3); break;
                    }
                break;
                case 1: 
                    switch (previouslyActivePanel) {
                        case 0: p = (buttonColorOrder[o] == 4); break;
                        case 2: p = (buttonColorOrder[o] == 1); break;
                        case 3: p = (buttonColorOrder[o] == 0); break;
                        case 4: p = (buttonColorOrder[o] == 3); break;
                        case 5: p = (buttonColorOrder[o] == 2); break;
                    }
                break;
                case 2: 
                    switch (previouslyActivePanel) {
                        case 0: p = (keypadLetterOrder[o] == 0); break;
                        case 1: p = (keypadLetterOrder[o] == 1); break;
                        case 3: p = (keypadLetterOrder[o] == 2); break;
                        case 4: p = (keypadLetterOrder[o] == 4); break;
                        case 5: p = (keypadLetterOrder[o] == 3); break;
                    }
                break;
                case 3: 
                    switch (previouslyActivePanel) {
                        case 0: p = (o == 0); break;
                        case 1: p = (o == 1); break;
                        case 2: p = (o == 2); break;
                        case 4: p = (o == 3); break;
                        case 5: p = (o == 4); break;
                    }
                break;
                case 4: 
                    switch (previouslyActivePanel) {
                        case 0: p = (dialShapeOrder[4-o] == 2); break;
                        case 1: p = (dialShapeOrder[4-o] == 1); break;
                        case 2: p = (dialShapeOrder[4-o] == 0); break;
                        case 3: p = (dialShapeOrder[4-o] == 3); break;
                        case 5: p = (dialShapeOrder[4-o] == 4); break;
                    }
                break;
                case 5: 
                    switch (previouslyActivePanel) {
                        case 0: p = (o == 1); break;
                        case 1: p = (o == 2); break;
                        case 2: p = (o == 0); break;
                        case 3: p = (o == 3); break;
                        case 4: p = (o == 4); break;
                    }
                break;
            }
        }
        if (p) {
            int[] r = {1, 3, 2, 0, 4, 4, 0, 3, 2, 1, 2, 4, 0, 3, 1};
            switch (n) {
                case 0: number *= (ulong)Math.Pow(primes[stage-1], wireColorOrder[o]); break;
                case 1: number *= (ulong)Math.Pow(primes[stage-1], buttonLabelOrder[o]); break;
                case 2: number *= (ulong)Math.Pow(primes[stage-1], r[o]); break;
                case 3: number *= (ulong)Math.Pow(primes[stage-1], r[switchColorOrder[o]+5]); break;
                case 4: number *= (ulong)Math.Pow(primes[stage-1], dialRotation); break;
                case 5: number *= (ulong)Math.Pow(primes[stage-1], r[Array.IndexOf(arrowOrder, o)+10]); break;
            }
            previouslyActivePanel = n;
            stage += 1;
            if (stage == 7) {
                StartCoroutine(HideAndSolve());
            } else {
                StartCoroutine(RevealNext());
            }
        } else {
            GetComponent<KMBombModule>().HandleStrike();
            Debug.LogFormat("[Mini #{0}] Stage {1}: Input was invalid, Strike!", moduleId, stage);
        }
    }

    IEnumerator RevealNext()
	{
		yield return new WaitForSeconds(.3f);
        PanelCovers[previouslyActivePanel].SetActive(true);
        ObjectSets[previouslyActivePanel].SetActive(false); 
        PanelCovers[previouslyActivePanel].GetComponent<MeshRenderer>().material = Colors[5];
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.WireSequenceMechanism, transform);
        yield return new WaitForSeconds(.3f);
        switch(chosenOrder[stage-1]) {
            case '1': PanelCovers[0].SetActive(false); ObjectSets[0].SetActive(true); break;
            case '2': PanelCovers[1].SetActive(false); ObjectSets[1].SetActive(true); break;
            case '3': PanelCovers[2].SetActive(false); ObjectSets[2].SetActive(true); break;
            case '4': PanelCovers[3].SetActive(false); ObjectSets[3].SetActive(true); break;
            case '5': PanelCovers[4].SetActive(false); ObjectSets[4].SetActive(true); break;
            case '6': PanelCovers[5].SetActive(false); ObjectSets[5].SetActive(true); break;
        }
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.WireSequenceMechanism, transform);
    }

    IEnumerator HideAndSolve()
	{
		yield return new WaitForSeconds(.3f);
        PanelCovers[previouslyActivePanel].SetActive(true);
        ObjectSets[previouslyActivePanel].SetActive(false); 
        PanelCovers[previouslyActivePanel].GetComponent<MeshRenderer>().material = Colors[5];
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.WireSequenceMechanism, transform);
        yield return new WaitForSeconds(.3f);
        Debug.LogFormat("[Mini #{0}] Validated all panels, module solved! {1}", moduleId, number);
        moduleSolved = true;
        GetComponent<KMBombModule>().HandlePass();
        yield return new WaitForSeconds(.3f);
        PaperNumber.text = number.ToString();
        Paper.SetActive(true);
        for (int q = 0; q < 10; q++) {
            yield return new WaitForSeconds(.05f);
            Audio.PlaySoundAtTransform("k"+UnityEngine.Random.Range(0,11).ToString(), transform);
            Paper.transform.localPosition += new Vector3(0.00587f, 0f, 0.00448f);
        }
    }
}
