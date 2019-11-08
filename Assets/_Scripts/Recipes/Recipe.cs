﻿using System;
using UnityEngine;
using Cooking;

namespace Recipes
{
    [System.Serializable]
    public struct Recipe
    {
        public GameObject recipeObject;
        public CookableIngredient mainIngredient;
        public CookableTopping[] toppings;
    }
}