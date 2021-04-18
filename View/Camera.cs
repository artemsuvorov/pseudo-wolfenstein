using PseudoWolfenstein.Core;
using PseudoWolfenstein.Model;
using PseudoWolfenstein.Utils;
using System;
using System.Drawing;

namespace PseudoWolfenstein.View
{
    public class Camera
    {
        private readonly Viewport viewport;
        private readonly Raycast raycast;
        private readonly Scene scene;
        private readonly Player player;

        public Camera(Viewport viewport, Scene scene)
        {
            this.viewport = viewport;
            this.scene = scene;
            this.player = scene.Player;
            this.raycast = new Raycast(this.scene);
        }

        public void DrawView(Graphics graphics)
        {
            using var backgroundBrush = new SolidBrush(Settings.ViewportBackgroundColor);
            graphics.FillRectangle(backgroundBrush, viewport.ClientRectangle);
            graphics.TranslateTransform(viewport.X, viewport.Y);

            var raycastData = raycast.GetCrossingPointsAndDistances();
            var crossingPoints = raycastData.CrossingPoints;
            var distances = raycastData.CrossingPointsDistances;
            var crossedObstacles = raycastData.CrossedObstacles;
            var crossedSides = raycastData.CrossedSides;
            if (raycastData is null || raycastData.Length <= 0) return;

            var sliceCount = raycastData.Length;
            var sliceWidth = viewport.Width / (float)sliceCount;
            for (var i = 0; i < sliceCount; i++)
            {
                float dst = distances[i];
                var ceiling = 0.5f * viewport.Height - Player.FieldOfView * viewport.Height / dst;
                var floor = viewport.Height - ceiling;
                var wallHeight = floor - ceiling;
                if (wallHeight < 1e-5) continue;

                var rayDir = raycastData.Rays[i].End.SafeNormalize();

                var crossedSide = crossedSides[i];
                var wallX = crossedSide == Side.Vertical ? player.Y + dst * rayDir.Y : player.X + dst * rayDir.X;
                wallX -= MathF.Floor(wallX);

                var texture = Repository.Textures.StoneWall;

                var d = ceiling * 64f - viewport.Height * 32f + wallHeight * 32f;
                var texX = wallX * texture.Width;
                var texY = d * texture.Height / wallHeight / 64;

                var destRect = new RectangleF(i*sliceWidth, ceiling, sliceWidth, wallHeight);
                var sourceRect = new RectangleF(texX, texY, 1f, texture.Height);
                graphics.DrawImage(texture, destRect, sourceRect, GraphicsUnit.Pixel);
            }

            graphics.ResetTransform();
        }

        //public void DrawView2(Graphics graphics)
        //{
        //    //ClearFrame(buffer);
        //    using var backgroundBrush = new SolidBrush(Settings.ViewportBackgroundColor);
        //    graphics.FillRectangle(backgroundBrush, viewport.ClientRectangle);
        //    graphics.TranslateTransform(viewport.X, viewport.Y);
        //    var raycastData = raycast.GetCrossingPointsAndDistances();

        //    var cameraPlane = Vector2.UnitY.RotateCounterClockwise(player.Rotation);
        //    for (int i = 0; i < viewport.Width; i++)
        //    {
        //        // This var tracks the relative position of the ray on the camera plane, from -1 to 1, with 0 being screen centre
        //        // so that we can use it to muliply the half-length of the camera plane to get the right direction of the ray.
        //        float cameraAngle = 2.0f * (i / (float)viewport.Width) - 1.0f;
        //        // This vector holds the direction the current ray is pointing.
        //        Vector2 rayDir = new Vector2(
        //            player.MotionDirection.X + cameraPlane.X * cameraAngle,
        //            player.MotionDirection.Y + cameraPlane.Y * cameraAngle);
        //        //var rayIndex = i / (viewport.Width / (float)Settings.RaycastRayCount);
        //        //var rayDir = raycastData.Rays[(int)rayIndex].End.SafeNormalize();
        //        // This holds the absolute SQUARE of the map the ray is in, regardless of position
        //        // within that square.
        //        int mapX = (int)player.X;
        //        int mapY = (int)player.Y;
        //        // These two variables track the distance to the next side of a map square from the player, 
        //        // e.g where the ray touches the horizontal side of a square, the distance is sideDistX and vertical square sideDistY.
        //        double sideDistX;
        //        double sideDistY;
        //        // These two variables are the distance between map square side intersections
        //        double deltaDistX = Math.Abs(1 / rayDir.X);
        //        double deltaDistY = Math.Abs(1 / rayDir.Y);
        //        // This var is for the overall length of the ray calculations
        //        double perpWallDist;

        //        // Each time we check the next square we step either 1 in the x or 1 in the y, they will be 1 or -1 depending on whether 
        //        // the character is facing towards the origin or away.
        //        int stepX;
        //        int stepY;

        //        // Finally, these two track whether a wall was hit, and the side tracks which side, horizontal or vertical was hit.
        //        // A horizontal side givess 0 and a vertical side is 1.
        //        bool hit = false;
        //        int side = 0;

        //        // Now we calculate the way we will step based upon the direction the character is facing
        //        // And the initial sideDist based upon this direction, and the deltaDist
        //        if (rayDir.X < 0)
        //        {
        //            stepX = -1;
        //            sideDistX = (player.X - mapX) * deltaDistX;
        //        }
        //        else
        //        {
        //            stepX = 1;
        //            sideDistX = (mapX + 1.0 - player.X) * deltaDistX;
        //        }
        //        if (rayDir.Y < 0)
        //        {
        //            stepY = -1;
        //            sideDistY = (player.Y - mapY) * deltaDistY;
        //        }
        //        else
        //        {
        //            stepY = 1;
        //            sideDistY = (mapY + 1.0 - player.Y) * deltaDistY;
        //        }

        //        //Now we loop steping until we hit a wall
        //        while (!hit)
        //        {
        //            // Here we check which distance is closer to us, x or y, and increment the lesser
        //            if (sideDistX < sideDistY)
        //            {
        //                // Increase the distance we've travelled.
        //                sideDistX += deltaDistX;
        //                // Set the ray's mapX to the new square we've reached.
        //                mapX += stepX;
        //                // Set it so the side we're currently on is an X side.
        //                side = 0;
        //            }
        //            else
        //            {
        //                sideDistY += deltaDistY;
        //                mapY += stepY;
        //                side = 1;
        //            }
        //            // Check if the ray is not on the side of a square that is a wall.
        //            if (mapX < 0 || mapY < 0 || mapY >= worldMap.Length || mapX >= worldMap[0].Length) break;
        //            if (worldMap[mapY][mapX] == '#')
        //            {
        //                hit = true;
        //            }
        //        }
        //        if (!hit) continue;

        //        //var rayIndex = i / (viewport.Width / (float)Settings.RaycastRayCount);
        //        //var crossingPosition = raycastData.CrossingPoints[(int)rayIndex];
        //        //var crossedPolygon = raycastData.CrossedObstacles[(int)rayIndex];
        //        //var mapp = new Vector2(mapX, mapY);
        //        //side = raycastData.CrossedSides[(int)rayIndex] == Side.Horizontal ? 0 : 1;
        //        // Now we've found where the next wall is, we have to find the actual distance.
        //        if (side == 0)
        //            perpWallDist = ((mapX - player.X + ((1 - stepX) / 2)) / rayDir.X);
        //        else
        //            perpWallDist = ((mapY - player.Y + ((1 - stepY) / 2)) / rayDir.Y);
        //        if (perpWallDist <= 0 || double.IsNaN(perpWallDist)) continue;

        //        // Here we'll start drawing the column of pixels, now we know what, and how far away.
        //        // First we find the height of the wall, e.g how much of the screen it should take up
        //        int columnHeight = (int)(viewport.Height / perpWallDist);
        //        // Next we need to find where to start drawing the column and where to stop, since the walls
        //        // will be in the centre of the screen, finding the start and end is quite simple.
        //        int drawStart = ((viewport.Height / 2) + (columnHeight / 2));
        //        // If we are going to be drawing off-screen, then draw just on screen.
        //        int drawEnd = ((viewport.Height / 2) - (columnHeight / 2));

        //        // Now we pick the colour to draw the line in, this is based upon the colour of the wall
        //        // and is then made darker if the wall is x aligned or y aligned.

        //        var texture = Repository.Textures.StoneWall;
        //        double wallX;

        //        if (side == 0)
        //            wallX = player.Y + perpWallDist * rayDir.Y;
        //        else
        //            wallX = player.X + perpWallDist * rayDir.X;

        //        wallX -= Math.Floor(wallX);

        //        int texX = (int)(wallX * (double)(texture.Width));
        //        if (side == 0 && rayDir.X > 0 || side == 1 && rayDir.Y < 0)
        //            texX = texture.Width - texX - 1;

        //        //for (int y = drawEnd; y < drawStart; y++)
        //        //{
        //        //    int d = y * 256 - viewport.Height * 128 + columnHeight * 128;
        //        //    int texY = ((d * texture.Height) / columnHeight) / 256;
        //        //    using var brush = new SolidBrush(texture.GetPixel(texX, texY));
        //        //    graphics.FillRectangle(brush, i, y, 1, 1);
        //        //}
        //        var destRect = new RectangleF(i, drawEnd, 1, columnHeight);
        //        var sourceRect = new RectangleF(texX, 0, 1f, texture.Height);
        //        graphics.DrawImage(texture, destRect, sourceRect, GraphicsUnit.Pixel);
        //    }
        //}

        //public void DrawView(Graphics graphics)
        //{
        //    //ClearFrame(buffer);
        //    using var backgroundBrush = new SolidBrush(Settings.ViewportBackgroundColor);
        //    graphics.FillRectangle(backgroundBrush, viewport.ClientRectangle);
        //    graphics.TranslateTransform(viewport.X, viewport.Y);

        //    for (int i = 0; i < viewport.Width; i++)
        //    {
        //        // This var tracks the relative position of the ray on the camera plane, from -1 to 1, with 0 being screen centre
        //        // so that we can use it to muliply the half-length of the camera plane to get the right direction of the ray.
        //        float cameraX = 2.0f * (i / (float)viewport.Width) - 1.0f;
        //        var cameraPlane = Vector2.UnitY.RotateCounterClockwise(player.Rotation);
        //        // This vector holds the direction the current ray is pointing.
        //        Vector2 rayDir = new Vector2(
        //            player.MotionDirection.X + cameraPlane.X * cameraX,
        //            player.MotionDirection.Y + cameraPlane.Y * cameraX);

        //        // This holds the absolute SQUARE of the map the ray is in, regardless of position
        //        // within that square.
        //        int mapX = (int)player.X;
        //        int mapY = (int)player.Y;
        //        // These two variables track the distance to the next side of a map square from the player, 
        //        // e.g where the ray touches the horizontal side of a square, the distance is sideDistX and vertical square sideDistY.
        //        double sideDistX;
        //        double sideDistY;
        //        // These two variables are the distance between map square side intersections
        //        double deltaDistX = Math.Abs(1 / rayDir.X);
        //        double deltaDistY = Math.Abs(1 / rayDir.Y);
        //        // This var is for the overall length of the ray calculations
        //        double perpWallDist;

        //        // Each time we check the next square we step either 1 in the x or 1 in the y, they will be 1 or -1 depending on whether 
        //        // the character is facing towards the origin or away.
        //        int stepX;
        //        int stepY;

        //        // Finally, these two track whether a wall was hit, and the side tracks which side, horizontal or vertical was hit.
        //        // A horizontal side givess 0 and a vertical side is 1.
        //        bool hit = false;
        //        int side = 0;

        //        // Now we calculate the way we will step based upon the direction the character is facing
        //        // And the initial sideDist based upon this direction, and the deltaDist
        //        if (rayDir.X < 0)
        //        {
        //            stepX = -1;
        //            sideDistX = (player.X - mapX) * deltaDistX;
        //        }
        //        else
        //        {
        //            stepX = 1;
        //            sideDistX = (mapX + 1.0 - player.X) * deltaDistX;
        //        }
        //        if (rayDir.Y < 0)
        //        {
        //            stepY = -1;
        //            sideDistY = (player.Y - mapY) * deltaDistY;
        //        }
        //        else
        //        {
        //            stepY = 1;
        //            sideDistY = (mapY + 1.0 - player.Y) * deltaDistY;
        //        }

        //        // Now we loop steping until we hit a wall
        //        while (!hit)
        //        {
        //            // Here we check which distance is closer to us, x or y, and increment the lesser
        //            if (sideDistX < sideDistY)
        //            {
        //                // Increase the distance we've travelled.
        //                sideDistX += deltaDistX;
        //                // Set the ray's mapX to the new square we've reached.
        //                mapX += stepX;
        //                // Set it so the side we're currently on is an X side.
        //                side = 0;
        //            }
        //            else
        //            {
        //                sideDistY += deltaDistY;
        //                mapY += stepY;
        //                side = 1;
        //            }
        //            // Check if the ray is not on the side of a square that is a wall.
        //            if (mapX < 0 || mapY < 0 || mapY >= worldMap.Length || mapX >= worldMap[0].Length) break;
        //            if (worldMap[mapY][mapX] == '#')
        //            {
        //                hit = true;
        //            }
        //        }
        //        if (!hit) continue;

        //        // Now we've found where the next wall is, we have to find the actual distance.
        //        if (side == 0)
        //            perpWallDist = ((mapX - player.X + ((1 - stepX) / 2)) / rayDir.X);
        //        else
        //            perpWallDist = ((mapY - player.Y + ((1 - stepY) / 2)) / rayDir.Y);

        //        // Here we'll start drawing the column of pixels, now we know what, and how far away.
        //        // First we find the height of the wall, e.g how much of the screen it should take up
        //        int columnHeight = (int)(viewport.Height / perpWallDist);
        //        // Next we need to find where to start drawing the column and where to stop, since the walls
        //        // will be in the centre of the screen, finding the start and end is quite simple.
        //        int drawStart = ((viewport.Height / 2) + (columnHeight / 2));
        //        // If we are going to be drawing off-screen, then draw just on screen.
        //        if (drawStart >= viewport.Height)
        //        {
        //            drawStart = viewport.Height - 1;
        //        }
        //        int drawEnd = ((viewport.Height / 2) - (columnHeight / 2));
        //        if (drawEnd < 0)
        //        {
        //            drawEnd = 0;
        //        }

        //        // Now we pick the colour to draw the line in, this is based upon the colour of the wall
        //        // and is then made darker if the wall is x aligned or y aligned.

        //        var texture = Repository.Textures.StoneWall;
        //        double wallX;

        //        if (side == 0)
        //            wallX = player.Y + perpWallDist * rayDir.Y;
        //        else
        //            wallX = player.X + perpWallDist * rayDir.X;

        //        wallX -= Math.Floor(wallX);

        //        int texX = (int)(wallX * (double)(texture.Width));
        //        if (side == 0 && rayDir.X > 0)
        //            texX = texture.Width - texX - 1;
        //        else
        //            texX = texture.Width - texX - 1;

        //        for (int y = drawEnd; y < drawStart; y++)
        //        {
        //            int d = y * 256 - viewport.Height * 128 + columnHeight * 128;
        //            int texY = ((d * texture.Height) / columnHeight) / 256;

        //            using var brush = new SolidBrush(texture.GetPixel(texX, texY));
        //            graphics.FillRectangle(brush, i, y, 1, 1);
        //            //using var pen = new Pen(texture.GetPixel(texX, texY));
        //            //buffer.SetPixel(i, y, texture.GetPixel(texX, texY));
        //            //DrawPixel(i, y, pen, buffer);
        //        }
        //    }
        //}

        //private void DrawPixel(int x, int y, Pen pen, Bitmap frame)
        //{
        //    using var g = Graphics.FromImage(frame);
        //    using var b = new SolidBrush(pen.Color);
        //    g.FillRectangle(b, x, y, 1, 1);
        //}

        //private void ClearFrame(Bitmap frame)
        //{
        //    using Graphics g = Graphics.FromImage(frame);
        //    g.Clear(Color.Black);
        //    //SolidBrush b = new SolidBrush(Color.Black);
        //    //g.FillRectangle(b, 0, 0, frame.Width, frame.Height);
        //}
        //public void DrawView2(Graphics graphics)
        //{
        //    using var backgroundBrush = new SolidBrush(Settings.ViewportBackgroundColor);
        //    graphics.FillRectangle(backgroundBrush, viewport.ClientRectangle);
        //    graphics.TranslateTransform(viewport.X, viewport.Y);

        //    var raycastData = raycast.GetCrossingPointsAndDistances();
        //    var crossingPoints = raycastData.CrossingPoints;
        //    var distances = raycastData.CrossingPointsDistances;
        //    var crossedObstacles = raycastData.CrossedObstacles;
        //    var crossedSides = raycastData.CrossedSides;
        //    if (raycastData is null || raycastData.Length <= 0) return;

        //    var sliceCount = raycastData.Length;
        //    //var sliceWidth = MathF.Round(viewport.Width / (float)sliceCount + 1f);
        //    //var sliceWidth = viewport.Width / (float)sliceCount;
        //    var sliceWidth = viewport.Width / (float)sliceCount;
        //    for (var i = 0; i < sliceCount; i++)
        //    {
        //        float dst = distances[i];
        //        var ceiling = MathF.Round(viewport.Height * 0.5f - Player.FieldOfView * viewport.Height / dst);
        //        var floor = viewport.Height - ceiling;
        //        var wallHeight = floor - ceiling;
        //        if (wallHeight < 1e-5) continue;
        //        var wallRectangle = new RectangleF(0.5f + i * (sliceWidth - 1f), ceiling, sliceWidth, wallHeight);

        //        var texture = Repository.Textures.StoneWall;
        //        //var offset = crossingPoints[i].X % texture.Width;
        //        //var scale = texture.Width / Settings.WorldWallSize;

        //        //var distance = Settings.RaycastRayCount / (2 * MathF.Tan(Settings.PlayerFieldOfView * 0.5f));
        //        //var projCoefficient = 3 * distance * Settings.WorldWallSize;

        //        //var destRect = new RectangleF(i * sliceWidth, ceiling, sliceWidth, wallHeight);
        //        //var sourceRect = new RectangleF(i * sliceWidth % texture.Width, 0, sliceWidth, texture.Height);
        //        //graphics.DrawImage(texture, destRect, sourceRect, GraphicsUnit.Pixel);
        //        //float cameraX = 2.0f * (i / (float)viewport.Width) - 1.0f;
        //        //var cameraPlane = Vector2.UnitY.RotateCounterClockwise(player.Rotation);
        //        // This vector holds the direction the current ray is pointing.
        //        Vector2 rayDir = raycastData.Rays[i].End.SafeNormalize();

        //        var crossedSide = crossedSides[i];
        //        var wallX = crossedSide == Side.Vertical ? 
        //            player.Y + dst * rayDir.Y :
        //            player.X + dst * rayDir.X;
        //        wallX -= MathF.Floor(wallX);

        //        int texX = (int)(wallX * texture.Width);
        //        //if (crossedSide == Side.Horizontal && rayDir.X > 0 || crossedSide == Side.Vertical && rayDir.Y < 0)
        //        //    texX = texture.Width - texX - 1;
        //        //else
        //        //    texX = texture.Width - texX - 1;

        //        //for (int y = (int)ceiling; y < floor; y++)
        //        //{
        //        //    int d = y * 64 - viewport.Height * 32 + (int)wallHeight * 32;
        //        //    int texY = ((d * texture.Height) / (int)wallHeight) / 64;
        //        //    //using var pen = new Pen(texture.GetPixel(texX, texY));
        //        //    //buffer.SetPixel(i, y, texture.GetPixel(texX, texY));
        //        //    //using var brush = new SolidBrush(texture.GetPixel(texX, texY));
        //        //    //graphics.FillRectangle(brush, i*sliceWidth, y, sliceWidth, 1);
        //        //    //DrawPixel(i, y, pen, buffer);
        //        //}
        //        var destRect = new RectangleF(i*sliceWidth, ceiling, sliceWidth, wallHeight);
        //        var sourceRect = new RectangleF(texX, 0, 1f, texture.Height);
        //        graphics.DrawImage(texture, destRect, sourceRect, GraphicsUnit.Pixel);
        //    }

        //    //DrawWallTextureInCenter(graphics, 16);
        //    graphics.ResetTransform();
        //}

        //public Bitmap ResizeImage(Image image, int width, int height)
        //{
        //    var destRect = new Rectangle(0, 0, width, height);
        //    var destImage = new Bitmap(width, height);

        //    destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

        //    using (var graphics = Graphics.FromImage(destImage))
        //    {
        //        graphics.CompositingMode = CompositingMode.SourceCopy;
        //        graphics.CompositingQuality = CompositingQuality.HighQuality;
        //        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //        graphics.SmoothingMode = SmoothingMode.HighQuality;
        //        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

        //        using var wrapMode = new ImageAttributes();
        //        wrapMode.SetWrapMode(WrapMode.TileFlipXY);
        //        graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
        //    }

        //    return destImage;
        //}

        // older version of draw function -- without texturing
        //public void DrawView(Graphics graphics)
        //{
        //    using var backgroundBrush = new SolidBrush(Settings.ViewportBackgroundColor);
        //    graphics.FillRectangle(backgroundBrush, viewport.ClientRectangle);
        //    graphics.TranslateTransform(viewport.X, viewport.Y);

        //    var raycastData = raycast.GetCrossingPointsAndDistances();
        //    var distances = raycastData.CrossingPointsDistances;
        //    var crossedObstacles = raycastData.CrossedObstacles;
        //    if (distances is null || distances.Length <= 0) return;

        //    var sliceCount = distances.Length;
        //    var sliceWidth = viewport.Width / (float)sliceCount + 1f;
        //    for (var i = 0; i < sliceCount; i++)
        //    {
        //        float dst = distances[i];
        //        var ceiling = MathF.Max(0f, MathF.Round(viewport.Height * 0.5f - Player.FieldOfView * viewport.Height / dst));
        //        var floor = viewport.Height - ceiling;
        //        var wallHeight = floor - ceiling;
        //        var wallRectangle = new RectangleF(0.5f + i * (sliceWidth - 1f), ceiling, sliceWidth, wallHeight);

        //        using var wallFillBrush = new SolidBrush(Settings.GameObjectFillColor);
        //        graphics.FillRectangle(wallFillBrush, wallRectangle);
        //    }

        //    graphics.ResetTransform();
        //}

        //private void DrawWallTextureInCenter(Graphics graphics, int sliceCount)
        //{
        //    var center = viewport.Center - texture.Size / 2;
        //    var sliceWidth = texture.Width / sliceCount;

        //    for (var i = 0; i < sliceCount; i++)
        //    {
        //        var destRect = new Rectangle(center.X+sliceWidth*i, center.Y-i*2, sliceWidth, texture.Height+i*4);
        //        var sourceRect = new Rectangle(sliceWidth*i, 0, sliceWidth, texture.Height);
        //        graphics.DrawImage(texture, destRect, sourceRect, GraphicsUnit.Pixel);
        //    }
        //}
    }
}