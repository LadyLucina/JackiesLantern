using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizedStarters : MonoBehaviour
{
    //***********************************************************
    // THIS SCRIPT IS MADE TO WORK WITH ROGUE MATERIAL AND ROGUE
    // LAYERED CARD SHADER.
    // --Certain textures have been rendered in grayscale with a
    // color tint multiplied over them. This script allows for
    // the color tint to be randomly changed.
    // --Accent clothing color changes first and the weapon accent
    // color is coordinated to it.
    // --Metals of the clothing and weapon are randomly selected
    // from a predefined list of colors.
    // --Eye color is randomly selected from a predefined list of colors.
    // --Body color is selected from a list of textures that is exposed
    // in the inspector (to make it easy to drag and drop them).
    // ---*This is how weapon lineart and weapon textures will later be assigned
    // ---*This is also the basis for how future classes might work. Maybe.

    //FOR TESTING PURPOSES:
    // Pressing SPACE BAR will rerandomize everything
    //***********************************************************

    //gives access to the renderer component
    private Renderer rend;

    //body colors
    [SerializeField] //This allows us to easily put these in via the inspector
    private Texture2D[] catBodies; //these are the textures that are the different colors for the base bodies

    //metallic colors predefined (colors are on a 0-1 float, rgb. Can also be rgba, if only 3, a is assumed 1)
    //only know your colors on a 256 scale? divide the number by 255 to get the value.
    private Color bronze =    new Color(.87f, .59f, .17f);
    private Color gold =      new Color(.99f, .76f, .29f);
    private Color whiteGold = new Color(.94f, .87f, .64f);
    private Color roseGold =  new Color(.89f, .56f, .53f);
    private Color silver =    new Color(.45f, .55f, .6f);
    private Color steel =     new Color(.29f, .32f, .35f);
    private Color platinum =      Color.white;
    private Color[] metallicColors; //because of "issues" you can't populate the array here

    //eye colors predefined (colors are on a 0-1 float, rgb. Can also be rgba, if only 3, a is assumed 1)
    //only know your colors on a 256 scale? divide the number by 255 to get the value.
    private Color lime =      new Color(.59f, 1f, .1f);
    private Color water =     new Color(.8f, .99f, 1f);
    private Color china =     new Color(.47f, .62f, .82f);
    private Color yellow =    new Color(.9f, .84f, .02f);
    private Color brown =     new Color(.67f, .55f, .38f);
    private Color purple =    new Color(.85f, .68f, 1f);
    private Color orange =    new Color(1f, .47f, 0f);
    private Color[] eyeColors; //because of "issues" you can't populate the array here


    // Start is called before the first frame update
    void Start()
    {
        //fill the predefined color arrays
        metallicColors = new Color[] { bronze, gold, whiteGold, roseGold, silver, steel, platinum };
        eyeColors = new Color[] { lime, water, china, yellow, brown, purple, orange };

        //get the renderer
        rend = GetComponent<Renderer>();

        //Random.ColorHSV() is an option for a random color...but I'm not sure if I like it.
        //rend.material.SetColor("_eyeColor", (Random.ColorHSV(0f, 1f, .25f, 1f, .5f, 1f)));
        
        //These call directly to the SHADER VARIABLES
        //fur texture
        rend.material.SetTexture("_bodyTexture", catBodies[Random.Range(0, catBodies.Length - 1)]);

        //Why am I making color variables? Because I might want to use them later, but will probably just combine them
        //eye color
        Color eyeTint = eyeColors[Random.Range(0, eyeColors.Length - 1)];
        rend.material.SetColor("_eyeColor", eyeTint);

        //accent color
        Color accentColor = new Color(Random.Range(0f, .75f), Random.Range(0f, .75f), Random.Range(0f, .75f), 1f);
        rend.material.SetColor("_clothesRandomColor", accentColor);

        //weapon accent color
        //a "quick" formula for finding a complementary color is that every value of two colors should equal 255
        //there are some issues with this, such as red is now partnered with cyan. But it's a decent formula for
        //quick, easy color coordinates that aren't usually the same. Since I throttle it so that the top can't be
        //full 255 (1), I reduce it to so that each value needs to add up to .75f. You can find some other cool 
        //combinations of matching colors by shifting the equation so that it's the .75-b, .75-r, .75-g or
        //.75-g, .75-b, .75-r
        Color weaponRandColor = new Color((.75f - accentColor.r), (.75f - accentColor.g), (.75f - accentColor.b), 1f);
        rend.material.SetColor("_weaponRandColor", weaponRandColor);

        //clothes metal color
        Color clothesMetalColor = metallicColors[Random.Range(0, metallicColors.Length - 1)];
        rend.material.SetColor("_clothesMetalColor", clothesMetalColor);

        //weapon metal color
        Color weaponMetalColor = metallicColors[Random.Range(0, metallicColors.Length - 1)];
        rend.material.SetColor("_weaponMetal", weaponMetalColor);

    }

    // Update is called once per frame
    void Update()
    {
        //TESTING RANDOMNESS
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //These call directly to the SHADER VARIABLES
            //fur texture
            rend.material.SetTexture("_bodyTexture", catBodies[Random.Range(0, catBodies.Length - 1)]);

            //Why am I making color variables? Because I might want to use them later, but will probably just combine them
            //eye color
            Color eyeTint = eyeColors[Random.Range(0, eyeColors.Length - 1)];
            rend.material.SetColor("_eyeColor", eyeTint);

            //accent color
            Color accentColor = new Color(Random.Range(0f, .75f), Random.Range(0f, .75f), Random.Range(0f, .75f), 1f);
            rend.material.SetColor("_clothesRandomColor", accentColor);

            //weapon accent color
            //a "quick" formula for finding a complementary color is that every value of two colors should equal 255
            //there are some issues with this, such as red is now partnered with cyan. But it's a decent formula for
            //quick, easy color coordinates that aren't usually the same. Since I throttle it so that the top can't be
            //full 255 (1), I reduce it to so that each value needs to add up to .75f. You can find some other cool 
            //combinations of matching colors by shifting the equation so that it's the .75-b, .75-r, .75-g or
            //.75-g, .75-b, .75-r
            Color weaponRandColor = new Color((.75f - accentColor.r), (.75f - accentColor.g), (.75f - accentColor.b), 1f);
            rend.material.SetColor("_weaponRandColor", weaponRandColor);

            //clothes metal color
            Color clothesMetalColor = metallicColors[Random.Range(0, metallicColors.Length - 1)];
            rend.material.SetColor("_clothesMetalColor", clothesMetalColor);

            //weapon metal color
            Color weaponMetalColor = metallicColors[Random.Range(0, metallicColors.Length - 1)];
            rend.material.SetColor("_weaponMetal", weaponMetalColor);
            

        }

    }
}
