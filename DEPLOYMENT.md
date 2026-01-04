# Deployment Guide - BabyShop3Berlian

## 🚀 Production Deployment

### Prerequisites
- Server dengan .NET 8.0 Runtime
- Web server (IIS, Nginx, atau Apache)
- SSL Certificate untuk HTTPS
- Domain name

### 1. Prepare for Production

#### Update appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=babyshop_production.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "yourdomain.com,www.yourdomain.com"
}
```

#### Update Program.cs for Production
```csharp
// Add security headers
app.UseHsts();
app.UseHttpsRedirection();

// Add security middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
```

### 2. Build for Production

```bash
# Clean previous builds
dotnet clean

# Build in Release mode
dotnet build --configuration Release

# Publish application
dotnet publish --configuration Release --output ./publish
```

### 3. Database Setup

#### For SQLite (Development/Small Scale)
```bash
# Copy database file to server
cp babyshop.db /path/to/production/
```

#### For SQL Server (Production)
1. Update connection string in appsettings.json
2. Install SQL Server packages
3. Run migrations:
```bash
dotnet ef database update --configuration Release
```

### 4. Server Configuration

#### IIS Deployment
1. Install ASP.NET Core Hosting Bundle
2. Create IIS site pointing to publish folder
3. Configure application pool for .NET Core
4. Set up SSL certificate

#### Linux with Nginx
1. Copy published files to `/var/www/babyshop/`
2. Configure Nginx reverse proxy:

```nginx
server {
    listen 80;
    server_name yourdomain.com www.yourdomain.com;
    return 301 https://$server_name$request_uri;
}

server {
    listen 443 ssl;
    server_name yourdomain.com www.yourdomain.com;
    
    ssl_certificate /path/to/certificate.crt;
    ssl_certificate_key /path/to/private.key;
    
    location / {
        proxy_pass http://localhost:5055;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }
}
```

3. Create systemd service:
```ini
[Unit]
Description=BabyShop3Berlian Web App
After=network.target

[Service]
Type=notify
ExecStart=/usr/bin/dotnet /var/www/babyshop/BabyShopWeb2.dll
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=babyshop
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target
```

### 5. Security Configuration

#### HTTPS Setup
- Obtain SSL certificate (Let's Encrypt recommended)
- Configure HTTPS redirection
- Set secure cookie policies

#### Security Headers
Add to Program.cs:
```csharp
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
    await next();
});
```

### 6. Performance Optimization

#### Enable Response Compression
```csharp
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});
```

#### Configure Caching
```csharp
builder.Services.AddMemoryCache();
builder.Services.AddResponseCaching();
```

#### Static File Caching
```csharp
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=31536000");
    }
});
```

### 7. Monitoring & Logging

#### Application Insights (Azure)
```csharp
builder.Services.AddApplicationInsightsTelemetry();
```

#### File Logging
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    },
    "File": {
      "Path": "logs/babyshop-.txt",
      "LogLevel": "Information"
    }
  }
}
```

### 8. Backup Strategy

#### Database Backup
```bash
# SQLite backup
cp babyshop_production.db backups/babyshop_$(date +%Y%m%d_%H%M%S).db

# SQL Server backup
sqlcmd -S server -d BabyShop -Q "BACKUP DATABASE BabyShop TO DISK='backup.bak'"
```

#### Application Backup
```bash
# Backup application files
tar -czf babyshop_backup_$(date +%Y%m%d).tar.gz /var/www/babyshop/
```

## 🐳 Docker Deployment

### Dockerfile
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BabyShopWeb2.csproj", "."]
RUN dotnet restore "./BabyShopWeb2.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "BabyShopWeb2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BabyShopWeb2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BabyShopWeb2.dll"]
```

### Docker Compose
```yaml
version: '3.8'
services:
  babyshop:
    build: .
    ports:
      - "80:80"
      - "443:443"
    volumes:
      - ./data:/app/data
      - ./logs:/app/logs
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - ASPNETCORE_URLS=http://+:80;https://+:443
    restart: unless-stopped
```

### Build & Run
```bash
# Build image
docker build -t babyshop3berlian .

# Run container
docker run -d -p 80:80 -p 443:443 --name babyshop babyshop3berlian

# Or use docker-compose
docker-compose up -d
```

## ☁️ Cloud Deployment

### Azure App Service
1. Create App Service in Azure Portal
2. Configure deployment from GitHub/Azure DevOps
3. Set connection strings in Configuration
4. Enable Application Insights

### AWS Elastic Beanstalk
1. Create Elastic Beanstalk application
2. Upload deployment package
3. Configure environment variables
4. Set up RDS for database

### Google Cloud Run
1. Build container image
2. Push to Google Container Registry
3. Deploy to Cloud Run
4. Configure custom domain

## 🔧 Environment Configuration

### Development
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=babyshop_dev.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Debug"
    }
  }
}
```

### Staging
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=babyshop_staging.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

### Production
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=prod-server;Database=BabyShop;Trusted_Connection=true;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  }
}
```

## 📊 Health Checks

### Add Health Checks
```csharp
builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>();

app.MapHealthChecks("/health");
```

### Custom Health Check
```csharp
public class DatabaseHealthCheck : IHealthCheck
{
    private readonly ApplicationDbContext _context;
    
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.Database.CanConnectAsync(cancellationToken);
            return HealthCheckResult.Healthy();
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(ex.Message);
        }
    }
}
```

## 🚨 Troubleshooting

### Common Issues

#### 1. Database Connection Issues
- Check connection string
- Verify database server accessibility
- Check firewall settings

#### 2. Static Files Not Loading
- Verify wwwroot folder copied
- Check file permissions
- Verify static files middleware

#### 3. PDF Generation Issues
- Check iTextSharp dependencies
- Verify file system permissions
- Check temp directory access

#### 4. Session Issues
- Configure session timeout
- Check session storage
- Verify cookie settings

### Logs Location
- **IIS**: `C:\inetpub\logs\LogFiles\`
- **Linux**: `/var/log/nginx/` and application logs
- **Docker**: `docker logs container_name`

## 📈 Performance Monitoring

### Key Metrics to Monitor
- Response time
- Memory usage
- CPU usage
- Database connection pool
- Error rates
- User sessions

### Tools
- Application Insights (Azure)
- New Relic
- Datadog
- Prometheus + Grafana

## 🔄 CI/CD Pipeline

### GitHub Actions Example
```yaml
name: Deploy to Production

on:
  push:
    branches: [ main ]

jobs:
  deploy:
    runs-on: ubuntu-latest
    
    steps:
    - uses: actions/checkout@v2
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v1
      with:
        dotnet-version: 8.0.x
        
    - name: Restore dependencies
      run: dotnet restore
      
    - name: Build
      run: dotnet build --no-restore
      
    - name: Test
      run: dotnet test --no-build --verbosity normal
      
    - name: Publish
      run: dotnet publish -c Release -o ./publish
      
    - name: Deploy to server
      # Add deployment steps here
```

## 📋 Pre-Deployment Checklist

- [ ] All tests passing
- [ ] Database migrations ready
- [ ] SSL certificate configured
- [ ] Environment variables set
- [ ] Backup strategy in place
- [ ] Monitoring configured
- [ ] Error handling tested
- [ ] Performance optimized
- [ ] Security headers configured
- [ ] Health checks implemented