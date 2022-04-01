Here's dialogue that plays after you finish the first quest.

->quest_list

=== quest_list ===
May I interest you in these side quests? 
*Sidequest 1 -> quest1
*Sidequest 2 -> quest2
*Sidequest 3 -> quest3

=== quest1 ===
Aaaa

BBbb

*Yes
Okay, quest accepted.
->DONE

*No 
Okay, quest refused. Wanna try another?
->quest_list

=== quest2 ===
*Yes
Okay, quest accepted.
->DONE

**No 
Okay, quest refused. Wanna try another?
->quest_list

=== quest3 ===
*Yes
Okay, quest accepted.
->DONE

*No 
Okay, quest refused. Wanna try another?
->quest_list


    -> END
