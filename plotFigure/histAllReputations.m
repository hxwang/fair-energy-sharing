
i = [5 10 15 20]
for k = 1:1:size(i,2)
j = i(1,k)/10
histReputation('CGAssigner',j)
histReputation('ProportionAssigner',j)
histReputation('PreferLargerDemandAssigner',j)
histReputation('PreferLargeReputationAssigner',j)
% histReputation('NoShareAssigner',j)
histReputation('EqualAssigner',j)
end

