using System.Collections.Generic;
using PureMVC.Patterns.Proxy;

namespace Game
{
    public class LevelProxy : Proxy
    {
        public new const string NAME = "LevelProxy";
        public IList<Block> Blocks
        {
            get {return (IList<Block>)base.Data;}
        }

        public LevelProxy() : base(NAME, new List<Block>()){}

        public void InitBlocks(LevelData data)
        {
            Blocks.Clear();
            for(var i = 0; i < data.blocks.Length; i++)
            {
                AddBlock(new Block(){id = i, toughness = data.blocks[i].toughness, position = data.blocks[i].position});
            }
        }

        public void AddBlock(Block item)
        {
            UpdateBlock(item);
            Blocks.Add(item);
        }

        public void DeleteBlock(Block item)
        {
            Blocks.Remove(item);
        }

        public void UpdateBlock(Block item) 
        {
            for(int i = 0; i < Blocks.Count; i++)
            {
                if (Blocks[i].id == item.id)
                {
                    Blocks[i] = item;
                    return;
                }
            }
        }
    }
}