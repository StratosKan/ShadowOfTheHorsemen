using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchForState : IState
{
    // "Search for" state for our A.I.
    // made by Stratos 

    private LayerMask searchLayer;                                // What layer are we looking for?
    private GameObject ownerGameObject;                           // Where is the starting position of the search?
    private float searchRadius;                                   // What is the search radius of our search unit?
    private string tagToLookFor;                                  // What is the tag we are looking for in our search?
    private System.Action<SearchResults> searchResultsCallBack;   // When the search is complete, what method shall I send the results to? (this is the best part of this script :P )

    public bool searchCompleted = false;                          // Search runs once, if you want to run it multiple times make sure u reset this boolean to true on your update method.

    public SearchForState (
        LayerMask searchLayer,GameObject ownerGameObject,float searchRadius,string tagToLookFor, Action<SearchResults> searchResultsCallBack
                                )                               //This is a constructor that ensures we get the required information when it's called.
    {
        this.searchLayer = searchLayer;
        this.ownerGameObject = ownerGameObject;
        this.searchRadius = searchRadius;
        this.tagToLookFor = tagToLookFor;
        this.searchResultsCallBack = searchResultsCallBack;
                
    }
    public void Enter()
    {
        //throw new System.NotImplementedException();
    }

    public void Execute()
    {
        if (!searchCompleted)                                               // Executing once (by default this boolean is false)
        {
            //Note: We can do it with sphereCast as well but it won't detect object already inside the sphereCast.

            var hitObjects = Physics.OverlapSphere(
                                      this.ownerGameObject.transform.position, this.searchRadius, this.searchLayer
                                                     );      //Detecting colliders within our search radius and inside our search layer.
            if (hitObjects != null && hitObjects.Length > 0)  //V2
            {

                var allHitObjectsWithRequiredTag = new List<Collider>();        // Creating a new list to add the objects we want in our search.

                for (int i = 0; i < hitObjects.Length; i++)                     // Using for loop over foreach one for optimization purposes.
                {
                    if (hitObjects[i].CompareTag(this.tagToLookFor))
                    {
                        //Debug.Log("Found a " + hitObjects[i].name);

                        allHitObjectsWithRequiredTag.Add(hitObjects[i]);        // Adding all object with the req tag in the list we created above.    
                        break;       //Stops the loop when it finds an object that fits the tag.  WE ONLY USE THIS FOR THE PLAYER SO FAR SO 1 OBJECT IS ENOUGH.
                    }
                }

                var searchResults = new SearchResults(hitObjects, allHitObjectsWithRequiredTag);     // We call the search results class and we provide all found information to it.

                this.searchResultsCallBack(searchResults);                                           // We send back the results to the client.
            }
            //else //STUPID ASS SOLUTION FIX SOON®
            //{
            //    var allHitObjectsWithRequiredTag = new List<Collider>();
            //    //var empty = new Collider empty;
            //    //allHitObjectsWithRequiredTag.Add(empty);
            //    var searchResults = new SearchResults(hitObjects, allHitObjectsWithRequiredTag);
            //    this.searchResultsCallBack(searchResults);
            //}
            this.searchCompleted = true;
        }
    }

    public void Exit()
    {
       //throw new System.NotImplementedException();
    }
    
}
public class SearchResults  //We contain this class in this script because its information is directly relevant to the action requested. 
{
    public Collider[] allHitObjectsInSearchRadius;

    public List<Collider> allHitObjectsWithRequiredTag;

    public SearchResults(Collider[] allHitObjectsInSearchRadius, List<Collider> allHitObjectsWithRequiredTag)  //This is a constructor that ensures we get the required information when it's called.
    {
        this.allHitObjectsInSearchRadius = allHitObjectsInSearchRadius;
        this.allHitObjectsWithRequiredTag = allHitObjectsWithRequiredTag;

        //method calls to further process this data
        //e.g. closest object || furthest object 

        //TODO: July 26, should add here the point of view!
        // from documentation 
        // All the bellow in a method call -.-
        //if (other)
        //{
        //    Vector3 forward = transform.TransformDirection(Vector3.forward);
        //    Vector3 toOther = other.position - transform.position;

        //    if (Vector3.Dot(forward, toOther) < 0)
        //    {
        //        print("The other transform is behind me!");
        //    }
        //}
    }
}
