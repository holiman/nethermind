using Nethermind.Core.Exceptions;
using Nethermind.Init.Steps;
using NUnit.Framework;

namespace Nethermind.Consensus.Test;

public class MiningConfigurationTests
{
    [Test]
    public void mining_configuration_updates_with_blocks_config_values()
    {
        IBlocksConfig blocksConfig = new BlocksConfig();
        IMiningConfig miningConfig = new MiningConfig();
        string testValue = "Test 123";
        blocksConfig.ExtraData = testValue;
        MigrateConfigs.MigrateBlocksConfig(blocksConfig, miningConfig.BlocksConfig);
        Assert.AreEqual(miningConfig.BlocksConfig.ExtraData, blocksConfig.ExtraData);

        //Change init order
        IMiningConfig miningConfig2 = new MiningConfig();
        IBlocksConfig blocksConfig2 = new BlocksConfig();

        string testValue2 = "Test 321";
        blocksConfig2.ExtraData = testValue2;
        MigrateConfigs.MigrateBlocksConfig(blocksConfig2, miningConfig2.BlocksConfig);
        Assert.AreEqual(miningConfig2.BlocksConfig.ExtraData, blocksConfig2.ExtraData);

    }
    [Test]
    public void blocks_configuration_updates_with_mining_config_values()
    {
        IBlocksConfig blocksConfig = new BlocksConfig();
        IMiningConfig miningConfig = new MiningConfig();
        string testValue = "Test 123";
        miningConfig.ExtraData = testValue;
        MigrateConfigs.MigrateBlocksConfig(blocksConfig, miningConfig.BlocksConfig);
        Assert.AreEqual(miningConfig.BlocksConfig.ExtraData, blocksConfig.ExtraData);

        //Change init order
        IMiningConfig miningConfig2 = new MiningConfig();
        IBlocksConfig blocksConfig2 = new BlocksConfig();

        string testValue2 = "Test 321";
        miningConfig2.ExtraData = testValue2;
        MigrateConfigs.MigrateBlocksConfig(blocksConfig2, miningConfig2.BlocksConfig);
        Assert.AreEqual(miningConfig2.BlocksConfig.ExtraData, blocksConfig2.ExtraData);
    }

    [Test]
    public void If_blocks_configuration_conflicts_with_mining_config_values_throw()
    {
        IMiningConfig miningConfig = new MiningConfig();
        IBlocksConfig blocksConfig = new BlocksConfig();

        string testValueA = "Test 123";
        blocksConfig.ExtraData = testValueA;

        string testValueB = "Test 000";
        miningConfig.ExtraData = testValueB;

        Assert.Throws<InvalidConfigurationException>(() => MigrateConfigs.MigrateBlocksConfig(blocksConfig, miningConfig.BlocksConfig));
    }
}
