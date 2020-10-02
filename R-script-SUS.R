library(rlang)
library(MASS)
library(fitdistrplus)
library(magrittr)
library(dplyr)
library(lazyeval)
library(parallel)
library(e1071)
library(plotly)
library(ggplot2)
library(triangle)
library(sqldf)
#library(readxl)
# library(knitr)
# library(rmarkdown)
library(simmer)
library(simmer.plot)

data<-read.csv("SUS.csv")

hist(data$Sum, prob =TRUE, main='Results Histogram',xlim = c(20,100) ,xlab='SUS score',ylab = 'Frequency',col='red')
lines(density(data$Sum),col='black',lwd=2)
abline(v=mean(data$Sum),col="blue",lty=3)

op <- par(cex = 1)

legend("topleft", c(paste("mean - ",format(mean(data$Sum),digits = 4, nsmall = 2)) ), col=c("blue"), lwd=2,lty=3,cex = 0.8,)


qqnorm(data$Sum)
qqline(data$Sum)

shapiro.test(data$Sum)

