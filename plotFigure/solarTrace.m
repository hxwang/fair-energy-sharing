

%Solar_High=[0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,1.21,10.85,20.20,45.51,84.70,125.39,168.80,210.99,256.81,301.72,345.73,386.72,429.22,471.42,509.70,549.49,584.76,621.53,652.88,682.42,709.54,735.16,764.40,815.94,856.64,877.13,717.68,853.62,711.95,565.16,403.00,227.27,295.99,349.35,294.19,322.52,530.50,247.16,494.03,683.92,659.51,592.59,214.61,373.46,259.52,418.37,336.08,209.79,282.73,148.30,83.49,40.09,30.14,17.48,25.32,7.23,0.60,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,1.81,9.34,17.48,42.20,81.08,126.30,173.92,215.82,261.93,306.24,351.46,402.70,450.62,396.67,280.02,233.00,400.29,268.87,115.14,183.26,232.70,191.70,284.84,158.55,298.41,923.25,196.53,254.40,514.83,523.87,454.24,406.31,430.73,511.81,739.99,841.26,831.32,611.28,702.91,667.04,632.98,598.32,561.55,523.57,480.46,437.36,393.96,349.35,305.04,259.52,213.71,162.16,112.13,68.72,33.46,13.56,3.62,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00];

Solar_High = load('..\data\processedTrace\solar.txt');

Solar_High = Solar_High'

maxNum = 0;
for i=1:1:size(Solar_High,2)
    if maxNum < Solar_High(1,i)
        maxNum = Solar_High(1,i)
    end
end

maxNum

solar_loadFactor = 1;

X=[1:1:size(Solar_High,2)];




% Create figure
figure1 = figure;
%comment out, otherwise, figure will be normalized
%set(figure1,'units','normalized','outerposition',[0 0 1 1]);


% Create axes
%axis([xmin xmax ymin ymax])
axes1 = axes('Parent',figure1);
box(axes1,'on');
hold(axes1,'all');
set(axes1,'FontSize',30,'FontWeight','bold');



% p= plot(X(1,:),Solar(1,:)/3)
% set(p,'Color','g','LineWidth',3,'linestyle','-')
% set(p,'Marker','^','Markersize',12);

p = area(Solar_High(1,:)/maxNum*100 *solar_loadFactor,'FaceColor','g');

 
   
%change x-axis scale 
axis([0 1440 0 100])
    
set(axes1,'XGrid','on','YGrid','off');
leg = legend(axes1,'show');
set(leg,'FontSize',30);
 
set(axes1,'YTick', [20,40,60,80,100], 'YTickLabel',{ 20,40,60,80,100},'XGrid','off','YGrid','off');
set(axes1,'XTick', [0:12*10: 12*24*5], 'XTickLabel',{ 0:10:24*5},'XGrid','off','YGrid','off');


% set(axes1,'XTick',[1:48*3:481*3],'XTickLabel',{'day-1 00:00','day-1 12:00','day-2 00:00','day-2 12:00','day-3 00:00','day-3 12:00','day-4 00:00','day-4 12:00','day-5 00:00','day-5 12:00','day-6 00:00'},'XGrid','on');
%set(axes1,'XTick',[48*3:48*3*2:481*3],'XTickLabel',{'day-1','day-2 ','day-3','day-4','day-5'},'XGrid','on');
%fix_xticklabels(gca,10,{'FontSize',30});

%legend
% legend('1K,Enumeration','1K,Approximation','5K,Enumeration','5K,Approximation','10K,Enumeration','10K,Approximation','100K,Enumeration','100K,Approximation')
legend('Solar Trace','location','northwest')

set(get(axes1,'XLabel'),'String','Time (Hour)','FontSize',30,'FontWeight','bold');
set(get(axes1,'YLabel'),'String','Solar power (kW)','FontSize',30,'FontWeight','bold');

%save to file
set(gcf, 'PaperPosition', [0 0 13 7]); %Position plot at left hand corner with width 5 and height 5.
set(gcf, 'PaperSize', [13 7]); %Set the paper to have width 5 and height 5.
%saveas(gcf, 'SolarTrace_High', 'pdf') %Save figure
saveas(gcf, '.\figures\SolarTrace_High', 'pdf') %Save figure  
saveas(gca, strcat('.\figures\SolarTrace_High', '.eps'),'psc2') %Save figure 
    
    
 
    
    
    
    
    
    
    
    
    
    
    