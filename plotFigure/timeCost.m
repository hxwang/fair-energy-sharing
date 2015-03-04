function [ y, delta] = timeCost(assigner,ratio)

%Solar_High=[0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,1.21,10.85,20.20,45.51,84.70,125.39,168.80,210.99,256.81,301.72,345.73,386.72,429.22,471.42,509.70,549.49,584.76,621.53,652.88,682.42,709.54,735.16,764.40,815.94,856.64,877.13,717.68,853.62,711.95,565.16,403.00,227.27,295.99,349.35,294.19,322.52,530.50,247.16,494.03,683.92,659.51,592.59,214.61,373.46,259.52,418.37,336.08,209.79,282.73,148.30,83.49,40.09,30.14,17.48,25.32,7.23,0.60,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,1.81,9.34,17.48,42.20,81.08,126.30,173.92,215.82,261.93,306.24,351.46,402.70,450.62,396.67,280.02,233.00,400.29,268.87,115.14,183.26,232.70,191.70,284.84,158.55,298.41,923.25,196.53,254.40,514.83,523.87,454.24,406.31,430.73,511.81,739.99,841.26,831.32,611.28,702.91,667.04,632.98,598.32,561.55,523.57,480.46,437.36,393.96,349.35,305.04,259.52,213.71,162.16,112.13,68.72,33.46,13.56,3.62,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00,0.00];


fileDir =  '..\simulationResult\';
alpha = 0.1;

fileName = strcat(fileDir, num2str(ratio*100),'\',assigner,'_timeCost.txt');
data = load(fileName);

y = mean(data);
delta = norminv(1-alpha/2)*std(data)/sqrt(size(data,1));

end



    
    
    
    
    
    
    
    