#include <ctype.h>
#include <stdio.h>
#include <cs50.h>
#include <string.h>

int main(int argc, string argv[])
{
    if (argc != 2)
    {
        printf("enter a key");
        return 1;
    }
    string key = argv[1];
    
    //validate key
    int length = 0;
    bool isValid = true;
    bool repeat = false;
    bool isThereNumber = false;
    int index;
    
    for (int i = 0; key[i] != 0; i++)
    {
        length++;
        
        //checks if the is a number in key
        if (isalpha(key[i]) && isThereNumber == false)
        {
            isThereNumber = false;
        }
        else
        {
            isThereNumber = true; 
        }
        //end
        
        //find a repeat
        for (int j = i + 1; key[j] != 0; j++)
        {
            if (toupper(key[j]) == toupper(key[i]) && !repeat)
            {
                repeat = true;
            } 
        }
        //end
    }
    
    if (length != 26)
    {
        printf("key needs to be 26 letters long");
        return 1;
    }
    
    if (repeat)
    {
        printf("no repeats plz!!");
        return 1;
    }
    
    if (isThereNumber)
    {
        printf("no numbers in key");
        return 1;
    }
    //end
    

    //convert to code
    
    string input = get_string("plaintext: ");
    printf("ciphertext: ");
    string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    
    for (int i = 0; input[i] != 0; i++)
    {
        if (islower(input[i]))
        {
            
            for (int j = 0; alphabet[j] != 0; j++)
            {
                if (alphabet[j] == toupper(input[i]))
                {
                    index = j;
                }
            }
            printf("%c", tolower(key[index]));
        }
        else if (isupper(input[i]))
        {
            
            for (int k = 0; alphabet[k] != 0; k++)
            {
                if (alphabet[k] == input[i])
                {
                    index = k;
                }
            }
            printf("%c", toupper(key[index]));
        }
        else 
        {
            printf("%c", input[i]);
        }
    }
    printf("\n");
    return 0;
}