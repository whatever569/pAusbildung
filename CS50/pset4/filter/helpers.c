#include "helpers.h"
#include <math.h>

// Convert image to grayscale
void grayscale(int height, int width, RGBTRIPLE image[height][width])
{
    int average;
    for (int i = 0; i < height; i++)
    {
        for (int j = 0; j < width; j++)
        {
            average = round(((float) image[i][j].rgbtBlue + image[i][j].rgbtRed + image[i][j].rgbtGreen)/3);
            image[i][j].rgbtBlue = average;
            image[i][j].rgbtGreen = average;
            image[i][j].rgbtRed = average;

        }
    }
    return;
}

// Convert image to sepia
void sepia(int height, int width, RGBTRIPLE image[height][width])
{
    for (int i = 0; i < height; i++)
    {
        for (int j = 0; j < width; j++)
        {
            int sr =  round(0.393 * image[i][j].rgbtRed + 0.769 * image[i][j].rgbtGreen + 0.189 * image[i][j].rgbtBlue);
            int sg = round(0.349 * image[i][j].rgbtRed + 0.686 * image[i][j].rgbtGreen + 0.168 * image[i][j].rgbtBlue);
            int sb = round(0.272 * image[i][j].rgbtRed + 0.534 * image[i][j].rgbtGreen + 0.131 * image[i][j].rgbtBlue);
            
            if (sr > 255) 
            {
                sr = 255;
            }
            if (sg > 255)
            { 
                sg = 255;
            }
            if (sb > 255) 
            {
                sb = 255;
            }
            
            image[i][j].rgbtRed = sr;
            image[i][j].rgbtGreen = sg;
            image[i][j].rgbtBlue = sb;
            
        }
    }
    return;
}

// Reflect image horizontally
void reflect(int height, int width, RGBTRIPLE image[height][width])
{
    int tmp1 = 0;
    int tmp2 = 0;
    int tmp3 = 0;
    
    int k;
    
    for (int i = 0; i < height; i++)
    {
        for (int j = 0; j < width / 2; j++)
        {
            k = (width - 1) - j;
            
            tmp1 = image[i][j].rgbtRed; 
            tmp2 = image[i][j].rgbtGreen; 
            tmp3 = image[i][j].rgbtBlue;
            
            image[i][j].rgbtRed = image[i][k].rgbtRed;
            image[i][j].rgbtGreen = image[i][k].rgbtGreen; 
            image[i][j].rgbtBlue = image[i][k].rgbtBlue;
            
            image[i][k].rgbtRed = tmp1; 
            image[i][k].rgbtGreen = tmp2; 
            image[i][k].rgbtBlue = tmp3;
        }
    }
    return;
}

int cornerOrEdge(int i, int j, int height, int width)
{
    //if is first row excluding corner l and r return 1
    if (i == 0 && j != 0 && j != width - 1)
    {
        return 1;
    }
    //if bottom row excluding corner l and r return 2
    else if (i == height - 1 && j != 0 && j != width - 1)
    {
        return 2;
    }
    //if top l corner return 3
    else if (i == 0 && j == 0)
    {
        return 3;
    }
    // if top r corner return 4
    else if (i == 0 && j == width - 1)
    {
        return 4;
    }
    //if bottom l corner return 5
    else if (i == height - 1 && j == 0)
    {
        return 5;
    }
    // if bottom r corner return 6
    else if (i == height - 1 && j == width - 1)
    {
        return 6;
    }
    //if l coloumn excluding top and bottom corners return 7
    else if (j == 0 && i != 0 && i != height - 1)
    {
        return 7;
    }
    //if r coloumn excluding top and bottom corners return 8
    else if (j == width - 1 && i != 0 && i != height - 1)
    {
        return 8;
    }
    else
    {
        return 0;
    }
}
// Blur image
void blur(int height, int width, RGBTRIPLE image[height][width])
{
    RGBTRIPLE copy[height][width];
    
    for (int q = 0; q < height; q++)
    {
        for (int u = 0; u < width; u++)
        {
            copy[q][u] = image[q][u];
        }
    }
    
    
    for (int i = 0; i < height; i++)
    {
        for (int j = 0; j < width; j++)
        {
            //mid pix
            if (cornerOrEdge(i, j, height, width) == 0)
            {
                float sur = 0; 
                float sug = 0;
                float sub = 0;
    
                for (int h = -1; h <= 1; h++)
                {
                    for (int w = -1; w <= 1; w++)
                    {
                        sur += copy[i + h][j + w].rgbtRed;
                        sug += copy[i + h][j + w].rgbtGreen;
                        sub += copy[i + h][j + w].rgbtBlue;
                    }
                }
                
                image[i][j].rgbtRed = round(sur / 9);
                image[i][j].rgbtGreen = round(sug / 9);
                image[i][j].rgbtBlue = round(sub / 9);
            }
            
            // first row excluding corner l and r
            else if (cornerOrEdge(i, j, height, width) == 1)
            {
                float sur = 0; 
                float sug = 0;
                float sub = 0;
                
                for (int h = 0; h <= 1; h++)
                {
                    for (int w = -1; w <= 1; w++)
                    {
                        sur += copy[i + h][j + w].rgbtRed;
                        sug += copy[i + h][j + w].rgbtGreen;
                        sub += copy[i + h][j + w].rgbtBlue;
                    }
                }
                
                image[i][j].rgbtRed = round(sur / 6);
                image[i][j].rgbtGreen = round(sug / 6);
                image[i][j].rgbtBlue = round(sub / 6);
            }
            
            // bottom row excluding corner l and r
            else if (cornerOrEdge(i, j, height, width) == 2)
            {
                float sur = 0; 
                float sug = 0;
                float sub = 0;
                
                for (int h = 0; h >= -1 ; h--)
                {
                    for (int w = -1; w <= 1; w++)
                    {
                        sur += copy[i + h][j + w].rgbtRed;
                        sug += copy[i + h][j + w].rgbtGreen;
                        sub += copy[i + h][j + w].rgbtBlue;
                    }
                }
                
                image[i][j].rgbtRed = round(sur / 6);
                image[i][j].rgbtGreen = round(sug / 6);
                image[i][j].rgbtBlue = round(sub / 6);
            }
            
            // top l corner
            else if (cornerOrEdge(i, j, height, width) == 3)
            {
                float sur = 0; 
                float sug = 0;
                float sub = 0;
                
                for (int h = 0; h <= 1; h++)
                {
                    for (int w = 0; w <= 1; w++)
                    {
                        sur += copy[i + h][j + w].rgbtRed;
                        sug += copy[i + h][j + w].rgbtGreen;
                        sub += copy[i + h][j + w].rgbtBlue;
                    }
                }
                
                image[i][j].rgbtRed = round(sur / 4);
                image[i][j].rgbtGreen = round(sug / 4);
                image[i][j].rgbtBlue = round(sub / 4);
            }
            
            // top r corner
            else if (cornerOrEdge(i, j, height, width) == 4)
            {
                float sur = 0; 
                float sug = 0;
                float sub = 0;
                
                for (int h = 0; h <= 1; h++)
                {
                    for (int w = -1; w <= 0; w++)
                    {
                        sur += copy[i + h][j + w].rgbtRed;
                        sug += copy[i + h][j + w].rgbtGreen;
                        sub += copy[i + h][j + w].rgbtBlue;
                    }
                }
                
                image[i][j].rgbtRed = round(sur / 4);
                image[i][j].rgbtGreen = round(sug / 4);
                image[i][j].rgbtBlue = round(sub / 4);
            }
            // bottom l corner
            else if (cornerOrEdge(i, j, height, width) == 5)
            {
                float sur = 0; 
                float sug = 0;
                float sub = 0;
                
                for (int h = 0; h >= -1; h--)
                {
                    for (int w = 0; w <= 1; w++)
                    {
                        sur += copy[i + h][j + w].rgbtRed;
                        sug += copy[i + h][j + w].rgbtGreen;
                        sub += copy[i + h][j + w].rgbtBlue;
                    }
                }
                
                image[i][j].rgbtRed = round(sur / 4);
                image[i][j].rgbtGreen = round(sug / 4);
                image[i][j].rgbtBlue = round(sub / 4);
            }
            // bottom r corner
            else if (cornerOrEdge(i, j, height, width) == 6)
            {
                float sur = 0; 
                float sug = 0;
                float sub = 0;
                
                for (int h = 0; h >= -1; h--)
                {
                    for (int w = -1; w <= 0; w++)
                    {
                        sur += copy[i + h][j + w].rgbtRed;
                        sug += copy[i + h][j + w].rgbtGreen;
                        sub += copy[i + h][j + w].rgbtBlue;
                    }
                }
                
                image[i][j].rgbtRed = round(sur / 4);
                image[i][j].rgbtGreen = round(sug / 4);
                image[i][j].rgbtBlue = round(sub / 4);
            }
            // l coloumn
            else if (cornerOrEdge(i, j, height, width) == 7)
            {
                float sur = 0; 
                float sug = 0;
                float sub = 0;
                
                for (int h = -1; h <= 1; h++)
                {
                    for (int w = 0; w <= 1; w++)
                    {
                        sur += copy[i + h][j + w].rgbtRed;
                        sug += copy[i + h][j + w].rgbtGreen;
                        sub += copy[i + h][j + w].rgbtBlue;
                    }
                }
                
                image[i][j].rgbtRed = round(sur / 6);
                image[i][j].rgbtGreen = round(sug / 6);
                image[i][j].rgbtBlue = round(sub / 6);
            }
            // r coloumn
            else if (cornerOrEdge(i, j, height, width) == 8)
            {
                float sur = 0; 
                float sug = 0;
                float sub = 0;
                for (int h = -1; h <= 1; h++)
                {
                    for (int w = -1; w <= 0; w++)
                    {
                        sur += copy[i + h][j + w].rgbtRed;
                        sug += copy[i + h][j + w].rgbtGreen;
                        sub += copy[i + h][j + w].rgbtBlue;
                    }
                }
                
                image[i][j].rgbtRed = round(sur / 6);
                image[i][j].rgbtGreen = round(sug / 6);
                image[i][j].rgbtBlue = round(sub / 6);
            }
        }
    }
    return;
}
