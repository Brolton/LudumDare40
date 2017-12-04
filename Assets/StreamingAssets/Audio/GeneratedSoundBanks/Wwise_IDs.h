/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID PLAY_BREATH = 3698047240U;
        static const AkUniqueID PLAY_DEATH = 1172822028U;
        static const AkUniqueID PLAY_FOOTSTEPS = 3854155799U;
        static const AkUniqueID PLAY_FUMBLE = 4244258261U;
        static const AkUniqueID PLAY_GAME_START = 4109843897U;
        static const AkUniqueID PLAY_HEARTBEAT = 3765695918U;
        static const AkUniqueID PLAY_LANTERN = 1133348338U;
        static const AkUniqueID PLAY_MONSTER_LOOP = 3543968233U;
        static const AkUniqueID PLAY_MONSTER_START = 1690946981U;
        static const AkUniqueID PLAY_PICKUPFIREFLY = 201742637U;
        static const AkUniqueID PLAY_STARTGAME = 1444205428U;
        static const AkUniqueID STOP_BREATH = 2593257294U;
        static const AkUniqueID STOP_FOOTSTEPS = 2963349357U;
        static const AkUniqueID STOP_HEARTBEAT = 3319673256U;
        static const AkUniqueID STOP_LANTERN = 180437504U;
        static const AkUniqueID STOP_MONSTER = 1495041834U;
        static const AkUniqueID STOP_MONSTER_ALL = 580469826U;
    } // namespace EVENTS

    namespace SWITCHES
    {
        namespace HEARTBEAT
        {
            static const AkUniqueID GROUP = 2179486487U;

            namespace SWITCH
            {
                static const AkUniqueID HIGH = 3550808449U;
                static const AkUniqueID LOW = 545371365U;
                static const AkUniqueID MIDDLE = 1026627430U;
            } // namespace SWITCH
        } // namespace HEARTBEAT

        namespace MOVEMENT
        {
            static const AkUniqueID GROUP = 2129636626U;

            namespace SWITCH
            {
                static const AkUniqueID RUN = 712161704U;
                static const AkUniqueID WALK = 2108779966U;
            } // namespace SWITCH
        } // namespace MOVEMENT

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID RTPC_FIREFLYDISTANCE = 1468078247U;
        static const AkUniqueID RTPC_HEARTBEAT = 2136815501U;
        static const AkUniqueID RTPC_MONSTERDISTANCE = 870318650U;
        static const AkUniqueID RTPC_MONSTERPOSITION = 2999519122U;
        static const AkUniqueID RTPC_MOVEMENT = 813601384U;
        static const AkUniqueID RTPC_MUSIC_VOLUME = 1596647065U;
        static const AkUniqueID RTPC_SFX_VOLUME = 932301089U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MUSIC = 3991942870U;
        static const AkUniqueID SFX = 393239870U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MASTER_SECONDARY_BUS = 805203703U;
        static const AkUniqueID MUSIC = 3991942870U;
        static const AkUniqueID SFX = 393239870U;
    } // namespace BUSSES

    namespace AUX_BUSSES
    {
        static const AkUniqueID RVB_MUSIC = 1187187701U;
        static const AkUniqueID RVB_SFX = 3349832385U;
    } // namespace AUX_BUSSES

}// namespace AK

#endif // __WWISE_IDS_H__
