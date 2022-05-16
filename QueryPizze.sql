SELECT ingrediente FROM Ingrediente 
JOIN IngredientePizza ON Ingrediente.Id = IngredientePizza.IngredientiId
WHERE ListaPizzeId = 2