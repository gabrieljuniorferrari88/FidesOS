import type { NextConfig } from 'next';

// Agora a configuração é limpa, sem o Sentry
const nextConfig: NextConfig = {
  images: {
    remotePatterns: [
      {
        protocol: 'https' as const,
        hostname: 'api.slingacademy.com',
        port: ''
      }
    ]
  },
  transpilePackages: ['geist']
};

export default nextConfig;