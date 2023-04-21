#include <stdio.h>
#include <stdlib.h>
#include <stdint.h>
#include <cs50.h>

typedef uint8_t BYTE;

int main(int argc, char *argv[])
{
    int counter = 0;
    char name[8];
    BYTE buffer[512];
    int bytes = 512;
    FILE *img = NULL;
    FILE *f;
    
    if (argc != 2)
    {
        printf("Usage: ./recover <file>");
        return 1;
    }
    
    f = fopen(argv[1], "r");
    
    if (f == NULL)
    {
        printf("bruh");
        return 1;
    }
    
    //till reach zero
    while (fread(buffer, bytes, 1, f))
    {
        if (buffer[0] == 0xff && buffer[1] == 0xd8 && buffer[2] == 0xff && (buffer[3] & 0xf0) == 0xe0 && counter == 0)
        {
            sprintf(name, "%03i.jpg", counter);
            img = fopen(name, "w");
            fwrite(buffer, bytes, 1, img);
            counter++;
        }
        else if (buffer[0] == 0xff && buffer[1] == 0xd8 && buffer[2] == 0xff && (buffer[3] & 0xf0) == 0xe0)
        {
            fclose(img);
            sprintf(name, "%03i.jpg", counter);
            img = fopen(name, "w");
            fwrite(buffer, bytes, 1, img);
            counter++;
        }
        else if (img != NULL)
        {
            fwrite(buffer, bytes, 1, img);
        }
    }
    fclose(f);
    fclose(img);
    
    return 0;
}
