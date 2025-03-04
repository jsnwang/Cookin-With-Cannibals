﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Serving
{
    public class TicketManagerGroup : MonoBehaviour
    {
        [Header("Tickets")]
        public TicketGroup[] ticketGroups;

        [Header("Counters")]
        public int numGroupsSpawned;
        public int numGroupsCompleted; // missed OR served
        public int numGroupsMissed;

        private int numActiveGroups;

        void Start()
        {

        }

        void Update()
        {
            // CleanupUnservedOrders();
        }

        public void CleanupUnservedOrders()
        {
            for (int i = 0; i < ticketGroups.Length; ++i)
            {
                TicketGroup ticketGroup = ticketGroups[i];

                if (ticketGroup.ContainsTickets() && ticketGroup.HasExpired())
                {
                    RemoveGroup(i);
                    numGroupsMissed++;
                }
            }
        }

        public void AddGroup(List<RecipeVariation> recipeVars, int lane)
        {
            if (ticketGroups[lane].ContainsTickets() == false)
            {
                ticketGroups[lane].SetTickets(recipeVars);
                numActiveGroups++;
                numGroupsSpawned++;
            }
        }

        public void AddGroup(List<RecipeVariation> recipeVars)
        {
            int lane = FindOpenLane();
            AddGroup(recipeVars, lane);
        }

        public void RemoveGroup(int lane)
        {
            if (ticketGroups[lane].ContainsTickets())
            {
                ticketGroups[lane].ClearTickets();
                numActiveGroups--;
                numGroupsCompleted++;
            }
        }

        public void RemoveGroup(RecipeGroup recipeGroup)
        {
            int oldestMatching = IndexOfOldestMatchingGroup(recipeGroup);
            RemoveGroup(oldestMatching);
        }

        public void RemoveAllGroups()
        {
            for (int i=0; i<ticketGroups.Length; ++i)
            {
                if (ticketGroups[i].ContainsTickets())
                {
                    RemoveGroup(i);
                }
            }
        }

        public bool HasRoomForNewTicket()
        {
            return numActiveGroups < 3;
        }

        public bool HasNoTickets()
        {
            return numActiveGroups == 0;
        }

        private int IndexOfOldestMatchingGroup(RecipeGroup recipeGroup)
        {
            List<float> agesOfMatchingGroups = new List<float>();
            foreach (TicketGroup ticketGroup in ticketGroups)
            {
                if (ticketGroup.ContainsTickets())
                {
                    float groupAge = ticketGroup.tableAge;
                    bool matchingGroup = ticketGroup.GetRecipeGroup().Equals(recipeGroup);

                    if (matchingGroup)
                    {
                        agesOfMatchingGroups.Add(groupAge);
                    }
                    else
                    {
                        agesOfMatchingGroups.Add(0f);
                    }
                }
                else
                {
                    agesOfMatchingGroups.Add(0f);
                }
            }
            
            int oldest = 0;
            for (int i=1; i<agesOfMatchingGroups.Count; ++i)
            {
                if (agesOfMatchingGroups[i] > agesOfMatchingGroups[oldest])
                {
                    oldest = i;
                }
            }

            return oldest;
        }

        private int FindOpenLane()
        {
            int leftMostOpen = 0;

            for (int i=0; i<ticketGroups.Length; ++i)
            {
                if (ticketGroups[i].ContainsTickets() == false)
                {
                    leftMostOpen = i;
                    break;
                }
            }

            return leftMostOpen;
        }

        /*
        public List<RecipeVariation> GetActiveTicketRecipes()
        {
            List<RecipeVariation> recipes = new List<RecipeVariation>();
            foreach (TicketPointv2 ticketPoint in ticketPoints)
            {
                if (ticketPoint.ContainsTicket())
                {
                    RecipeVariation recipeVar = ticketPoint.recipe;
                    recipes.Add(recipeVar);
                }
            }

            return recipes;
        }
        */

        public List<RecipeGroup> GetOrderedRecipeGroups()
        {
            List<RecipeGroup> recipeGroups = new List<RecipeGroup>();

            foreach (TicketGroup ticketGroup in ticketGroups)
            {
                if (ticketGroup.ContainsTickets())
                {
                    recipeGroups.Add(ticketGroup.GetRecipeGroup());
                }
            }

            return recipeGroups;
        }

    }
}