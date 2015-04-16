 
% i = [5 10 15 20]
% for k = 1:1:size(i,2)
% j = i(1,k)/10
% histReputation('CGAssigner',j)
% histReputation('ProportionAssigner',j)
% histReputation('PreferLargerDemandAssigner',j)
% histReputation('PreferLargeReputationAssigner',j)
% histReputation('NoShareAssigner',j)
% histReputation('EqualAssigner',j)
% end

i = 10;
j = i/10;
[fesTime, fesDelta] = timeCost('CGAssigner',j)
[eesTime, eesDelta] = timeCost('EqualAssigner', j)
[pesTime, pesDelta] = timeCost('ProportionAssigner',j)
[HDFTime, hdfDelta] = timeCost('PreferLargerDemandAssigner',j)
[HRFTime, hrfDelta] = timeCost('PreferLargeReputationAssigner',j)



%
homeCount = [10 50 100 150 200 250 300 ] 

fesTime = [ 83.8847, 566.3736,   1.3685e+03,  2.3402e+03,    3.5878e+03,  4.8976e+03, 6.2002e+03];
fesDelta = [ 1.5238 , 4.3243, 8.9416,   12.0012,   20.3890, 23.8761,  21.1727];

eesTime = [  64.1586, 356.5405, 794.0028,  1.2735e+03,   1.7955e+03,  2.2783e+03,  2.7922e+03];
eesDelta= [   0.7885,3.0791,   6.4447, 6.3495,  12.9130, 11.4924, 10.6778];

pesTime = [53.7339,  249.2583,   520.0516, 828.9579, 1.1420e+03,   1.4354e+03,  1.7606e+03];
pesDelta = [ 0.5871, 1.6510,  2.9376,  3.8308, 7.4458,   7.1358, 7.0793];

hdfTime = [ 67.8712, 512.4850,   1.1923e+03, 1.9509e+03, 2.7952e+03,  3.6029e+03,   4.5229e+03];

hdfDelta = [  0.9322,  3.7744, 8.1745,  9.4662, 17.1161,  15.5169,  10.9180];

hrfTime = [ 79.6303, 447.2316, 979.0418, 1.5655e+03, 2.1993e+03,   2.7955e+03,   3.4208e+03
];
hrfDelta = [  1.1813, 2.2639,   4.4749,  5.3942, 17.1115,   12.2023, 11.7095];

% Create figure
figure1 = figure;
%comment out, otherwise, figure will be normalized
%set(figure1,'units','normalized','outerposition',[0 0 1 1]);
% set(figure1,  'Visible', 'off')

% Create axes
%axis([xmin xmax ymin ymax])
axes1 = axes('Parent',figure1);
box(axes1,'on');
hold(axes1,'all');
%do not open show the figures
set(axes1,'FontSize',30,'FontWeight','bold');



p= plot(homeCount(1,:),fesTime(1,:));
set(p,'Color','b','LineWidth',3,'linestyle','-')
set(p,'Marker','o','Markersize',13);

p= plot(homeCount(1,:),eesTime(1,:));
set(p,'Color','g','LineWidth',3,'linestyle','--')
set(p,'Marker','>','Markersize',13);

p= plot(homeCount(1,:),pesTime(1,:));
set(p,'Color','r','LineWidth',3,'linestyle','-.')
set(p,'Marker','^','Markersize',13);

p= plot(homeCount(1,:),hdfTime(1,:));
set(p,'Color','m','LineWidth',3,'linestyle','-')
set(p,'Marker','d','Markersize',13);

p= plot(homeCount(1,:),hrfTime(1,:));
set(p,'Color','c','LineWidth',3,'linestyle','-')
set(p,'Marker','V','Markersize',13);

errorbar(homeCount, fesTime, fesDelta,'Color','b', 'LineWidth',2)
errorbar(homeCount, eesTime, eesDelta,'Color','g', 'LineWidth',2)
errorbar(homeCount, pesTime, pesDelta,'Color','r', 'LineWidth',2)
errorbar(homeCount, hdfTime, hdfDelta,'Color','m', 'LineWidth',2)
errorbar(homeCount, hrfTime, hrfDelta,'Color','c', 'LineWidth',2)

axis([9.9 300.1 0 7000])

set(axes1,'XGrid','on','YGrid','off');
legend(axes1,'show');
leg = legend('FET', 'EET', 'PET', 'HDF', 'HRF')
set(leg,'FontSize',26, 'location', 'northwest');

set(get(axes1,'XLabel'),'String','Home count','FontSize',30,'FontWeight','bold');
set(get(axes1,'YLabel'),'String','Running time (ms)','FontSize',30,'FontWeight','bold');

%save to file
set(gcf, 'PaperPosition', [0 0 13 7]); %Position plot at left hand corner with width 5 and height 5.
set(gcf, 'PaperSize', [13 7]); %Set the paper to have width 5 and height 5.
%saveas(gcf, 'SolarTrace_High', 'pdf') %Save figure
saveas(gcf, strcat('.\figures\timeCost'), 'pdf') %Save figure  
saveas(gca, strcat('.\figures\timeCost ','eps'),'psc2') %Save figure 
    