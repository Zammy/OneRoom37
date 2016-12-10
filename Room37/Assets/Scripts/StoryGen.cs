using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;


public class SymbolModel
{
    public char UnderlyingData 
    {
        get
        {
            return _underlayingData;
        }
        private set
        {
            _underlayingData = value;
        }
    }

   public SymbolModel (char data)
   {
        this.UnderlyingData = data;
   }

   public override string ToString()
    {
        return string.Format("{0}", UnderlyingData);
    }

   private char _underlayingData;
}

public class StoryModel
{
    public int MurdererIndex
    {
        get;
        private set;
    }

    public List<SymbolModel[]> PlayerStories
    {
        get;
        private set;
    }

    public StoryModel (int murderIndex, List<SymbolModel[]> playerStories )
    {
        this.MurdererIndex = murderIndex;
        this.PlayerStories = playerStories;
    }

    public override string ToString()
    {
        var strBuilder = new StringBuilder();
        foreach(var story in this.PlayerStories)
        {
            strBuilder.Append("\n");
            strBuilder.Append("{");
            strBuilder.Append("\t");
            for (int i = 0; i < story.Length; i++)
            {
                strBuilder.Append(story[i]);
            }
            strBuilder.Append("\t},");
        }
        return string.Format("[StoryModel: MurdererIndex={0}, PlayerStories={1}]", MurdererIndex, strBuilder);
    }
}

public class StoryGen
{
    public int PlayersCount
    { 
        get;
        set;
    }

    public char[] Alphabet
    {
        get;
        set;
    }

    public int StoryLength
    {
        get;
        set;
    }

    public StoryModel GenerateStories()
    {
        SymbolModel[] baseStory = GenBaseStory();
        int murdererIndex = UnityEngine.Random.Range(0, this.PlayersCount);
        var playerStories = new List<SymbolModel[]>(this.PlayersCount);
        for (int i = 0; i < this.PlayersCount; i++)
        {
            var playerStory = (SymbolModel[])baseStory.Clone();
            playerStory = ExchangeRandomSymbols(playerStory, (murdererIndex == i ? 2 : 1));
            playerStories.Add(playerStory);
        }
        return new StoryModel(murdererIndex, playerStories);
    }

    SymbolModel[] GenBaseStory()
    {
        var alphabet = new List<char>(this.Alphabet);

        var baseStory = new SymbolModel[this.StoryLength];
        for (int i = 0; i < this.StoryLength; i++)
        {
            int index = UnityEngine.Random.Range(0, alphabet.Count);
            char symbol = alphabet[index];
            alphabet.RemoveAt(index);
            baseStory[i] = new SymbolModel(symbol);
        }
        return baseStory;
    }

    SymbolModel[] ExchangeRandomSymbols(SymbolModel[] storyModel, int depth)
    {
        if (storyModel.Length <= depth) 
        {
            throw new UnityException("storyModel length must be bigger than depth");
        }

        var exchanges = new List<Tuple<int>>();
        for (int i = 0; i < depth; i++)
        {
            bool alreadyExist = false;
            Tuple<int> storyTuple;
            do
            {
                storyTuple = new Tuple<int>(storyModel.xRandomIndex(), storyModel.xRandomIndex());
                alreadyExist = exchanges.Any( t =>
                {
                    return storyTuple.CrossEqual(t);
                });
            }
            while (alreadyExist);
            exchanges.Add(storyTuple);
        }

        exchanges.ForEach( e =>
        {
            storyModel.xExchange(e.Item1, e.Item2);
        });

        return storyModel;
    }
}
