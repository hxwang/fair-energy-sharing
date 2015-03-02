i = [5 10 15 20]
for k = 1:1:size(i,2)
j = i(1,k)/10
inVSout('CGAssigner',j)
inVSout('ProportionAssigner',j)
inVSout('PreferLargerDemandAssigner',j)
inVSout('PreferLargeReputationAssigner',j)
inVSout('NoShareAssigner',j)
end


