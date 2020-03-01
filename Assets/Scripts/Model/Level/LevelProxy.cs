using System;
using System.Collections.Generic;
using PureMVC.Patterns.Proxy;

namespace Game
{
    public class LevelProxy : Proxy
    {
        public new const string NAME = "LevelProxy";
        private GameProxy gameProxy = null;
        public IList<Block> Blocks
        {
            get {return (IList<Block>)base.Data;}
        }

        public LevelProxy() : base(NAME, new List<Block>()){}

        public override void OnRegister()
        {
            base.OnRegister();
            gameProxy = Facade.RetrieveProxy(GameProxy.NAME) as GameProxy;
            if (null == gameProxy)
                throw new Exception("[LevelProxy][OnRegister] " + GameProxy.NAME + "is null!");
        }

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
            Blocks.Add(item);
        }

        public void DeleteBlock(Block item)
        {
            Blocks.Remove(item);
        }

        public void HitBlock(int id)
        {
            var block = FindBlock(id);
            if(block != null)
            {
                block.toughness--;
                if(block.toughness <= 0)
                {
                    gameProxy.AddScore(20);
                    SendNotification(GameEvent.REMOVE_BLOCK, block);
                    DeleteBlock(block);
                    if(!AnyBlock())
                    {
                        SendNotification(GameEvent.LEVEL_COMPLETE);
                    }
                }
                else
                {
                    gameProxy.AddScore(10);
                    SendNotification(GameEvent.UPDATE_BLOCK, block);
                }
            }
        }

        private bool AnyBlock()
        {
            return Blocks.Count > 0;
        }

        private Block FindBlock(int id)
        {
            Block res = null;
            for (int i = 0; i < Blocks.Count; i++)
            {
                if (Blocks[i].id == id)
                {
                    res = Blocks[i];
                    break;
                }
            }
            return res;
        }
    }
}